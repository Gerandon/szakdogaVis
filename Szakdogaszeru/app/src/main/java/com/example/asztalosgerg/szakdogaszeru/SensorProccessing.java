package com.example.asztalosgerg.szakdogaszeru;

import android.content.Context;
import android.content.DialogInterface;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ToggleButton;

import java.util.Timer;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

public class SensorProccessing extends AppCompatActivity implements SensorEventListener {


    Sensor accelerometer;
    SensorManager sm;
    TextView textResponse;
    Timer t;
    long startTime = 0;

    PrintWriter printWriter;
    Socket socket;

    TextView xText;
    TextView yText;
    TextView zText;

    public static EditText editTextAddress, editTextPort;
    TextView getText;
    String ipAddress;
    int portN;
    //Low-pass filtering
    private float timeConstant = 0.18f;
    private float alpha = 0.9f;
    private float dt = 0;
    // Timestamps for the low-pass filters
    private float timestamp = System.nanoTime();
    private float timestampOld = System.nanoTime();
    private float[] gravity = new float[]{ 0, 0, 0 };
    private float[] linearAcceleration = new float[]{ 0, 0, 0 };
    // Raw accelerometer data
    private float[] input = new float[]{ 0, 0, 0 };

    private int count = 0;

    Switch lpf = null;
    boolean lpfOnOrOff;

    RadioGroup radi;
    String radiovalue;
    Integer sensorvalue;

    ToggleButton tog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sensor_proccessing);
        //Hide title bar
        //this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        tog = (ToggleButton)findViewById(R.id.toggleButton);
        lpf = (Switch)findViewById(R.id.switch1);
        lpf.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener(){
                                          public void onCheckedChanged(CompoundButton buttonView, boolean isChecked){
              if(isChecked){
                  lpfOnOrOff=true;
              }
              else{
                  lpfOnOrOff=false;
              }
          }
        });
        sensorvalue = SensorManager.SENSOR_DELAY_UI;
        radi = (RadioGroup)findViewById(R.id.radioGr);
        radi.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                radiovalue = ((RadioButton)findViewById(radi.getCheckedRadioButtonId())).getText().toString();
                switch(radiovalue)
                {
                    case "UI": sensorvalue = SensorManager.SENSOR_DELAY_UI;
                        break;
                    case "Fastest":sensorvalue = SensorManager.SENSOR_DELAY_FASTEST;
                        //Toast.makeText(this), "this is my Toast message!!! =)",
                          //      Toast.LENGTH_LONG).show();
                        break;
                    case "Normal":sensorvalue=SensorManager.SENSOR_DELAY_NORMAL;
                        break;
                }
            }
        });

        xText = (TextView) findViewById(R.id.textViewX);
        yText = (TextView) findViewById(R.id.textViewY);
        zText = (TextView) findViewById(R.id.textViewZ);

        editTextAddress = Connection.editTextAddress;
        editTextPort = Connection.editTextPort;
        textResponse = (TextView) findViewById(R.id.response);
        getText = (TextView) findViewById(R.id.getText);
        InventMyClient(null);

        sm = (SensorManager) getSystemService(SENSOR_SERVICE);
        accelerometer = sm.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        sm.registerListener(this, accelerometer,30000);//UI, sensorvalue

        tog.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (isChecked) {
                    isToggleOn = true;
                } else {
                    isToggleOn = false;
                }
            }
        });

    }
    boolean isToggleOn = false;
    ///Változók a pozíció számításhoz
    static final float NS2S = 1.0f / 1000000000.0f;
    float[] last_values = null;
    float[] velocity = null;
    float[] position = null;
    long last_timestamp = 0;
    public float[] posOrVelocity(SensorEvent event,String choose)
    {
        if(last_values != null){
            float dt = (event.timestamp - last_timestamp) * NS2S;

            for(int index = 0; index < 3;++index){
                velocity[index] += (event.values[index] + last_values[index])/2 * dt;
                position[index] += velocity[index] * dt;
            }
        }
        else{
            last_values = new float[3];
            velocity = new float[3];
            position = new float[3];
            velocity[0] = velocity[1] = velocity[2] = 0f;
            position[0] = position[1] = position[2] = 0f;
        }
        System.arraycopy(event.values, 0, last_values, 0, 3);
        last_timestamp = event.timestamp;
        switch(choose) {
            case "position":
                return position;
            case "velocity":
                return velocity;
        }
        return null; //idáig nem jut el
    }
    @Override
    public void onSensorChanged(SensorEvent event) {
        if(isToggleOn){

            alpha = .8F;//timeConstant / (timeConstant + dt);
            gravity[0] = alpha * gravity[0] + (1 - alpha) * event.values[0];
            gravity[1] = alpha * gravity[1] + (1 - alpha) * event.values[1];
            gravity[2] = alpha * gravity[2] + (1 - alpha) * event.values[2];
            linearAcceleration[0] = posOrVelocity(event,"velocity")[0]-gravity[0];
            linearAcceleration[1] = posOrVelocity(event,"velocity")[1]-gravity[1];
            linearAcceleration[2] = posOrVelocity(event,"velocity")[2]-gravity[2];
            xText.setText("X: "+linearAcceleration[0]);
            yText.setText("Y: "+linearAcceleration[1]);
            zText.setText("Z: "+linearAcceleration[2]);
        }
        else{
            if(lpfOnOrOff==true){
                //low-pass filter
                timestamp=System.nanoTime();
                //Megkeressük a frissítések közötti periódust
                //Átalakítjuk nano-másodpercről másodperccé
                dt=1 / (count / ((timestamp - timestampOld) / 1000000000.0f));
                count++;
                alpha = 0.8F;//timeConstant / (timeConstant + dt);
                gravity[0] = alpha * gravity[0] + (1 - alpha) * event.values[0];
                gravity[1] = alpha * gravity[1] + (1 - alpha) * event.values[1];
                gravity[2] = alpha * gravity[2] + (1 - alpha) * event.values[2];
                //-----Low pass filter
                linearAcceleration[0] = event.values[1]-gravity[1];//Y tengelyt küldjük üresen
                linearAcceleration[1] = event.values[1];//y tengelyt küldjük szűrve
                linearAcceleration[2] = event.values[2] - gravity[2];

                startTime = System.currentTimeMillis();
                xText.setText("X: "+linearAcceleration[0]);
                yText.setText("Y: "+linearAcceleration[1]);
                zText.setText("Z: " + linearAcceleration[2]);
            }
            else{
                linearAcceleration[0] = event.values[0];
                linearAcceleration[1] = event.values[1];
                linearAcceleration[2] = event.values[2];
                startTime = -1;
                xText.setText("X: "+event.values[0]);
                yText.setText("Y: "+event.values[1]);
                zText.setText("Z: " + event.values[2]);
            }
        }


        try {
            printWriter=new PrintWriter(socket.getOutputStream());
            printWriter.println(linearAcceleration[0]+" "+ linearAcceleration[1]+" "+linearAcceleration[2]+" "+startTime);
            printWriter.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }catch (Exception e){
            textResponse.setText("");
        }
    }
    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }
    public void InventMyClient(View view){

        MyClient myClientTask = new MyClient(
                editTextAddress.getText().toString(),
                Integer.parseInt(editTextPort.getText().toString()));
        myClientTask.execute();
    }
    public class MyClient extends AsyncTask<Void, Void, Void> {

        String response = "";//Tartalmazza a hibát, mondjuk úgy
        MyClient(String addr, int port){
            ipAddress= addr;
            portN = port;
        }
        @Override
        protected Void doInBackground(Void... arg0) {

            try {
                socket = new Socket(ipAddress,portN);
                ByteArrayOutputStream byteArrayOutputStream =
                        new ByteArrayOutputStream(1024);
                byte[] buffer = new byte[1024];
                int bytesRead;
                InputStream inputStream = socket.getInputStream();
                while ((bytesRead = inputStream.read(buffer)) != -1){
                    byteArrayOutputStream.write(buffer, 0, bytesRead);
                    response += byteArrayOutputStream.toString("UTF-8");
                }

            } catch (UnknownHostException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
                response = "UnknownHostException: " + e.toString();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
                response = "IOException: " + e.toString();
            }finally{
                if(socket != null){
                    try {
                        socket.close();
                    } catch (IOException e) {
                        // TODO Auto-generated catch block
                        e.printStackTrace();
                    }
                }
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void result) {
            textResponse.setText("A kapcsolat megszakadt!!\n"+response);
            super.onPostExecute(result);
        }

    }
}
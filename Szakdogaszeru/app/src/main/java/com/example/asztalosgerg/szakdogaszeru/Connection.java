package com.example.asztalosgerg.szakdogaszeru;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class Connection extends AppCompatActivity {

    TextView textResponse;
    public static EditText editTextAddress, editTextPort;
    Button buttonConnect;
    TextView getText;
    public static Socket socket=null;
    String ipAddress;
    int portN;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_connection);
        editTextAddress=(EditText)findViewById(R.id.addressBox);
        editTextPort=(EditText)findViewById(R.id.portBox);
        //buttonConnect=(Button)findViewById(R.id.connectButton);
        //buttonConnect.setOnClickListener(buttonConnectOnClickListener);
        //textResponse=(TextView)findViewById(R.id.response);
        getText = (TextView) findViewById(R.id.getText);
    }
    public void connectButtonClicked(View view){
        Intent inti = new Intent(this,SensorProccessing.class);
        startActivity(inti);
    }

}

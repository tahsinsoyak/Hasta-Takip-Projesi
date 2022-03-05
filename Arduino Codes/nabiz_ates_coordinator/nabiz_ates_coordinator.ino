//xbee conf
#include <SoftwareSerial.h>
#define xBeeRxPin 7
#define xBeeTxPin 6
SoftwareSerial xBeeSerial(xBeeRxPin, xBeeTxPin);
char val;

//nabiz
#define USE_ARDUINO_INTERRUPTS true
#include "PulseSensorPlayground.h"
const int pulse = A0;
int esik_degeri = 540;
PulseSensorPlayground pulseSensor;
String nabiz_str="";

//sicaklik
//LM335
int outputPin= 5;
int cel=0;
String dht11_str="";
//LM35
int lm35Pin = A1;
int okunan_deger = 0;
int lm35 = 0;
String lm35_str="";
char alarm_kod='0';


int k=0;
void setup() {
  // put your setup code here, to run once:

  xBeeSerial.begin(9600);
  Serial.begin(9600);

  //nabiz
  pulseSensor.analogInput(pulse); 
  pulseSensor.setThreshold(esik_degeri);
  pulseSensor.begin();
}

void loop() {

  // put your main code here, to run repeatedly:
  int rawvoltage= analogRead(outputPin);
  float millivolts= (rawvoltage/1024.0) * 5000;
  
  float kelvin= (millivolts/10);
  float celsius= kelvin - 273.15;
  cel = (int)celsius;

  okunan_deger = analogRead(lm35Pin);
  lm35 = (okunan_deger / 1023.0)*30;
  
  int nabiz = pulseSensor.getBeatsPerMinute();
  delay(100);
  if (pulseSensor.sawStartOfBeat()) { 
   if(nabiz<100){
     nabiz_str="0" + String(nabiz);
   }
   else{
    nabiz_str=String(nabiz);
   }
   
   if(lm35<10){
    lm35_str = "0"+String(lm35);
   }
   else{
    lm35_str = String(lm35);
   }
   //                                                  değiştir!!!
   String message = "h "+nabiz_str+" "+String(cel)+" "+lm35_str;
   //String message = "h "+nabiz_str+" "+String(cel);
  alarm_kod=Serial.read();
   if(alarm_kod=='2')
   {
    message+=" 2";
   }
   else if(alarm_kod=='1'){
    message+=" 1";
   }
   else{
    message+=" 0";
   }
   
   char Buf[14];
   message.toCharArray(Buf, 14);
   xBeeSerial.write(Buf, 14);
   delay(100);
   Serial.println(message);
  }
  delay(1000);
}

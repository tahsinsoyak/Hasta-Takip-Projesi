#include <SoftwareSerial.h>
#define xBeeRxPin 7
#define xBeeTxPin 6

SoftwareSerial xBeeSerial(xBeeRxPin,xBeeTxPin);
char val;
String nabiz="",ates="",scklk="";
int sayac=0,sayac2=0;
void setup()
{
  Serial.begin(9600);
  xBeeSerial.begin(9600);
}
void loop()
{
  if(xBeeSerial.available())
  {
    val=xBeeSerial.read();
    //xBeeSerial.println(val);
    Serial.print(val);
    if(val=='h'){
      sayac=0;
      sayac2=0;
    }
    else if(val==' '){
      sayac=sayac+1;
    }
    else if(sayac==1){
      nabiz=nabiz+(String)val;
    }
    else if(sayac==2){
      ates=ates+(String)val;
    }
    else if(sayac==3){
      if(sayac2<2){
        scklk=scklk+(String)val;
        sayac2=sayac2+1;
      }
      else{
        sayac=sayac+1;
        Serial.print("\n");
        Serial.print(nabiz);
        Serial.print(ates);
        Serial.print(scklk);
        Serial.print("\n");
        nabiz="";
        ates="";
        scklk="";
      }
    }
    delay(20);
  }
  else
  {
    delay(100);
  }
}

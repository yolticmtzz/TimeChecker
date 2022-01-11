
String data;
char d1;
char c;
String appendSerialData;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  
//  if(Serial.available())
//  {
//    //data = Serial.readString();
//    //d1 = data.charAt(0);
//    appendSerialData += data;
//
//    if(d1 == 'h')
//    {
//      digitalWrite(LED_BUILTIN, LOW);
//      Serial.print("Arduino Say>> High");
//      Serial.println(appendSerialData);
//    }
//
//    else if (d1 == 'l')
//    {
//      digitalWrite(LED_BUILTIN, HIGH);
//      Serial.print("Arduino Say>> LOW");
//      Serial.println(appendSerialData);
//    }  

 while (Serial.available())
 {
    c = Serial.read();
    appendSerialData += c;  
 }

 if (c == '#')
 {
  Serial.print("Arduino Say>> ");
  Serial.println(appendSerialData);
  appendSerialData = ""; 
  c = 0; //d1
  }
      
        
}



    

#include <Adafruit_NeoPixel.h>

int ribbonPin = 6;
int ledCount = 60;

Adafruit_NeoPixel strip = Adafruit_NeoPixel(ledCount, ribbonPin, NEO_GRB + NEO_KHZ800);

String message;
 
void setup(){
  Serial.begin(9600);
  strip.begin();
  for (int i = 0; i < ledCount; i++ ) {
    strip.setPixelColor(i, 50, 50, 50);
  }
  strip.setBrightness(50);
  strip.show();
}

void loop(){
  if (Serial.available() > 0) {
    // read the incoming byte:
    message = Serial.readString();

    if(message == "Red")
    {
      for (int i = 0; i < ledCount; i++ ) {
        strip.setPixelColor(i, 200, 50, 50);
      }
    }

    if(message == "Green")
    {
      for (int i = 0; i < ledCount; i++ ) {
        strip.setPixelColor(i, 50, 200, 50);
      }
    }

    if(message == "Blue"){
      for (int i = 0; i < ledCount; i++ ) {
        strip.setPixelColor(i, 50, 50, 200);
      }
    }

    if(message == "Fuck you"){
      for (int i = 0; i < ledCount; i++ ) {
        strip.setPixelColor(i, 50, 50, 50);
      }
      message = "Go fuck yourself, YOU !";
    }

    // say what you got:
    Serial.print("Arduino : ");
    Serial.println(message);
    strip.show();
  }
}

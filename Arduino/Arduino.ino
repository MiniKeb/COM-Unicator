#include <Adafruit_NeoPixel.h>

int ribbonPin = 6;
int ledCount = 60;

Adafruit_NeoPixel strip = Adafruit_NeoPixel(ledCount, ribbonPin, NEO_GRB + NEO_KHZ800);

// "ROYAGTCLBVFPW";
uint32_t getColor(char letter){
  switch(letter)
  {
    case 'R': // R = Red
      return strip.Color(255, 0, 0);  
    case 'O': // O = Orange
      return strip.Color(255, 127, 0); 
    case 'Y': // Y = Yellow
      return strip.Color(255, 255, 0); 
    case 'A': // A = Apple Green
      return strip.Color(127, 255, 0); 
    case 'G': // G = Green
      return strip.Color(0, 255, 0); 
    case 'T': // T = Turquoise
      return strip.Color(0, 255, 127); 
    case 'C': // C = Cyan
      return strip.Color(0, 255, 255); 
    case 'L': // L = Light Blue
      return strip.Color(0, 127, 255); 
    case 'B': // B = Blue
      return strip.Color(0, 0, 255); 
    case 'V': // V = Violet
      return strip.Color(127, 0, 255); 
    case 'F': // F = Fushia
      return strip.Color(255, 0, 255); 
    case 'P': // P = Pink
      return strip.Color(255, 127, 255);
    case 'W': // W = White
      return strip.Color(255, 255, 255);
    default:
      return strip.Color(0,0,0);
  }
}

void stripDemoShow(){
  strip.setPixelColor(0, 127, 127, 127);
  for (int i = 0; i < ledCount; i++ ) {
    strip.setPixelColor(i, 127, 127, 127);
    strip.show();
    delay(100);
    strip.setPixelColor(i, 0, 0, 0);
  }

  for(char repeat = 0; repeat < 3; repeat++){
    for (int i = 0; i < ledCount; i++ ) {
      strip.setPixelColor(i, 127, 127, 127);
    }
    strip.show();
    
    delay(200);

    for (int i = 0; i < ledCount; i++ ) {
      strip.setPixelColor(i, 0, 0, 0);
    }  
    strip.show();
    
    delay(200);
  }
}

void setup(){
  Serial.begin(9600);
  strip.begin();
  strip.setBrightness(50);
  stripDemoShow();
}

String message;

void loop(){
  if (Serial.available() > 0) {
    char buffer[ledCount];
    
    if(Serial.readBytes(buffer, ledCount) == ledCount){
      for(int i = 0; i < ledCount; i++){
        uint32_t color = getColor(buffer[i]);
        strip.setPixelColor(i, color);
      }
      strip.show();
    }


    // // read the incoming byte:
    // message = Serial.readString();

    // Serial.print("Out > ");
    // Serial.print(message.length());
    // if(message.length() == ledCount + 1){
    //   Serial.print("In > ");
    //   for (int i = 0; i < ledCount; i++ ) {
    //     uint32_t color = getColor(message[i]);
    //     strip.setPixelColor(i, color);
    //   }
    // }
    // strip.show();

    Serial.print("Arduino : ");
    Serial.println(buffer);
  }
}

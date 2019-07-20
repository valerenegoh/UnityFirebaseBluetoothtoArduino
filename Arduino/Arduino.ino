char incomingData;
int C1_LED = 47;
int CS1_LED = 45;
int D1_LED = 43;
int DS1_LED = 41;
int E1_LED = 39;
int F1_LED = 37;
int FS1_LED = 35;
int G1_LED = 33;
int GS1_LED = 31;
int A1_LED = 29;
int AS1_LED = 27;
int B1_LED = 25;
int C2_LED = 46;
int CS2_LED = 44;
int D2_LED = 42;
int DS2_LED = 40;
int E2_LED = 38;
int F2_LED = 36;
int FS2_LED = 34;
int G2_LED = 32;
int GS2_LED = 30;
int A2_LED = 28;
int AS2_LED = 26;
int B2_LED = 24;
int C3_LED = 22;

void setup() {
  Serial.begin(9600);
  pinMode(C1_LED, OUTPUT);
  pinMode(CS1_LED, OUTPUT);
  pinMode(D1_LED, OUTPUT);
  pinMode(DS1_LED, OUTPUT);
  pinMode(E1_LED, OUTPUT);
  pinMode(F1_LED, OUTPUT);
  pinMode(FS1_LED, OUTPUT);
  pinMode(G1_LED, OUTPUT);
  pinMode(GS1_LED, OUTPUT);
  pinMode(A1_LED, OUTPUT);
  pinMode(AS1_LED, OUTPUT);
  pinMode(B1_LED, OUTPUT);
  pinMode(C2_LED, OUTPUT);
  pinMode(CS2_LED, OUTPUT);
  pinMode(D2_LED, OUTPUT);
  pinMode(DS2_LED, OUTPUT);
  pinMode(E2_LED, OUTPUT);
  pinMode(F2_LED, OUTPUT);
  pinMode(FS2_LED, OUTPUT);
  pinMode(G2_LED, OUTPUT);
  pinMode(GS2_LED, OUTPUT);
  pinMode(A2_LED, OUTPUT);
  pinMode(AS2_LED, OUTPUT);
  pinMode(B2_LED, OUTPUT);
  pinMode(C3_LED, OUTPUT);
  digitalWrite (C1_LED, LOW);
  digitalWrite (CS1_LED, LOW);
  digitalWrite (D1_LED, LOW);
  digitalWrite (DS1_LED, LOW);
  digitalWrite (E1_LED, LOW);
  digitalWrite (F1_LED, LOW);
  digitalWrite (FS1_LED, LOW);
  digitalWrite (G1_LED, LOW);
  digitalWrite (GS1_LED, LOW);
  digitalWrite (A1_LED, LOW);
  digitalWrite (AS1_LED, LOW);
  digitalWrite (B1_LED, LOW);
  digitalWrite (C2_LED, LOW);
  digitalWrite (CS2_LED, LOW);
  digitalWrite (D2_LED, LOW);
  digitalWrite (DS2_LED, LOW);
  digitalWrite (E2_LED, LOW);
  digitalWrite (F2_LED, LOW);
  digitalWrite (FS2_LED, LOW);
  digitalWrite (G2_LED, LOW);
  digitalWrite (GS2_LED, LOW);
  digitalWrite (A2_LED, LOW);
  digitalWrite (AS2_LED, LOW);
  digitalWrite (B2_LED, LOW);
  digitalWrite (C3_LED, LOW);
}

void loop() {
  if(Serial.available()>0){
    incomingData = Serial.read();
    Serial.println(incomingData);
    switch(incomingData){
      case '0':
        digitalWrite (C1_LED, HIGH);
        delay(100);
        digitalWrite(C1_LED, LOW);
        break;
      case '1':
        digitalWrite (CS1_LED, HIGH);
        delay(100);
        digitalWrite(CS1_LED, LOW);
        break;
      case '2':
        digitalWrite (D1_LED, HIGH);
        delay(100);
        digitalWrite(D1_LED, LOW);
        break;
      case '3':
        digitalWrite (DS1_LED, HIGH);
        delay(100);
        digitalWrite(DS1_LED, LOW);
        break;
      case '4':
        digitalWrite (E1_LED, HIGH);
        delay(100);
        digitalWrite(E1_LED, LOW);
        break;
      case '5':
        digitalWrite (F1_LED, HIGH);
        delay(100);
        digitalWrite(F1_LED, LOW);
        break;
      case '6':
        digitalWrite (FS1_LED, HIGH);
        delay(100);
        digitalWrite(FS1_LED, LOW);
        break;
      case '7':
        digitalWrite (G1_LED, HIGH);
        delay(100);
        digitalWrite(G1_LED, LOW);
        break;
      case '8':
        digitalWrite (GS1_LED, HIGH);
        delay(100);
        digitalWrite(GS1_LED, LOW);
        break;
      case '9':
        digitalWrite (A1_LED, HIGH);
        delay(100);
        digitalWrite(A1_LED, LOW);
        break;
      case 'a':
        digitalWrite (AS1_LED, HIGH);
        delay(100);
        digitalWrite(AS1_LED, LOW);
        break;
      case 'b':
        digitalWrite (B1_LED, HIGH);
        delay(100);
        digitalWrite(B1_LED, LOW);
        break;
      case 'c':
        digitalWrite (C2_LED, HIGH);
        delay(100);
        digitalWrite(C2_LED, LOW);
        break;
      case 'd':
        digitalWrite (CS2_LED, HIGH);
        delay(100);
        digitalWrite(CS2_LED, LOW);
        break;
      case 'e':
        digitalWrite (D2_LED, HIGH);
        delay(100);
        digitalWrite(D2_LED, LOW);
        break;
      case 'f':
        digitalWrite (DS2_LED, HIGH);
        delay(100);
        digitalWrite(DS2_LED, LOW);
        break;
      case 'g':
        digitalWrite (E2_LED, HIGH);
        delay(100);
        digitalWrite(E2_LED, LOW);
        break;
      case 'h':
        digitalWrite (F2_LED, HIGH);
        delay(100);
        digitalWrite(F2_LED, LOW);
        break;
      case 'i':
        digitalWrite (FS2_LED, HIGH);
        delay(100);
        digitalWrite(FS2_LED, LOW);
        break;
      case 'j':
        digitalWrite (G2_LED, HIGH);
        delay(100);
        digitalWrite(G2_LED, LOW);
        break;
      case 'k':
        digitalWrite (GS2_LED, HIGH);
        delay(100);
        digitalWrite(GS2_LED, LOW);
        break;
      case 'l':
        digitalWrite (A2_LED, HIGH);
        delay(100);
        digitalWrite(A2_LED, LOW);
        break;
      case 'm':
        digitalWrite (AS2_LED, HIGH);
        delay(100);
        digitalWrite(AS2_LED, LOW);
        break;
      case 'n':
        digitalWrite (B2_LED, HIGH);
        delay(100);
        digitalWrite(B2_LED, LOW);
        break;
      case 'o':
        digitalWrite (C3_LED, HIGH);
        delay(100);
        digitalWrite(C3_LED, LOW);
        break;      
    }
  }
}

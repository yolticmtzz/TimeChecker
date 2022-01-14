#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 5
#define RST_PIN 0


MFRC522 Batch(SS_PIN, RST_PIN); // Instance of the class

MFRC522::MIFARE_Key key;

byte batch_uid_checkIN[] = {0xA0, 0xCB, 0x84, 0x15};
byte batch_uid_checkOUT[] = {0xB0, 0xCB, 0x84, 0x15};

bool check = false;

void setup()
{
  Serial.begin(9600);
  SPI.begin();     // Init SPI bus
  Batch.PCD_Init(); // Init MFRC522

}

void loop()
{

  // Reset the loop if no new card present on the sensor/reader. This saves the entire process when idle.
  if (Batch.PICC_IsNewCardPresent() && Batch.PICC_ReadCardSerial())
  {

    if (check == false)
    {
      for (int j = 0; j < 4; j++)
      {
        // Batch nicht gleich Checkout ID ist, dann check = true
        if (Batch.uid.uidByte[j] != batch_uid_checkOUT[j])
        {
          Serial.print("Gelesene UID: ");
          for (byte i = 0; i < Batch.uid.size; i++)
          {
            Serial.print(Batch.uid.uidByte[i] < 0x10 ? "0" : " ");
            Serial.print(Batch.uid.uidByte[i], HEX);
          }
          Serial.print(" -> CheckIN");
          Serial.println();
          check = true;
        }
      }
    }

    else 
    {
      Serial.print("Gelesene UID: ");
      for (int x = 0; x < 4; x++)
      {
        Batch.uid.uidByte[x] = batch_uid_checkOUT[x];
        Serial.print(Batch.uid.uidByte[x] < 0x10 ? "0" : " ");
        Serial.print(Batch.uid.uidByte[x], HEX);
      }
      Serial.print(" -> CheckOUT");

      Serial.println();
      check = false;
    }


    
    // Halt PICC
    Batch.PICC_HaltA();
    delay(800);

    // Stop encryption on PCD
    Batch.PCD_StopCrypto1();
  }
}
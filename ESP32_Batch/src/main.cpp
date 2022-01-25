#include <SPI.h>
#include <MFRC522.h>

 /** Typical pin layout used:
 * -----------------------------------------------------------------------------------------
 *             MFRC522      Arduino       Arduino   Arduino    Arduino          Arduino
 *             Reader/PCD   Uno/101       Mega      Nano v3    Leonardo/Micro   Pro Micro
 * Signal      Pin          Pin           Pin       Pin        Pin              Pin
 * -----------------------------------------------------------------------------------------
 * RST/Reset   RST          9             5         D9         RESET/ICSP-5     RST
 * SPI SS      SDA(SS)      10            53        D10        10               10
 * SPI MOSI    MOSI         11 / ICSP-4   51        D11        ICSP-4           16
 * SPI MISO    MISO         12 / ICSP-1   50        D12        ICSP-1           14
 * SPI SCK     SCK          13 / ICSP-3   52        D13        ICSP-3           15
 *
 * More pin layouts for other boards can be found here: https://github.com/miguelbalboa/rfid#pin-layout
 * 
*/

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

  // Überprüfung ob neue Karte anwesend und diese gelesen werden kann
  if (Batch.PICC_IsNewCardPresent() && Batch.PICC_ReadCardSerial())
  {

    if (check == false)
    {
      for (int j = 0; j < 4; j++)
      {
        // Überprüfung ob Batch-ID-CheckIN ungleich Batch-ID-CheckOUT ist.
        if (Batch.uid.uidByte[j] != batch_uid_checkOUT[j])
        {
          Serial.println();
          Serial.print("Gelesene UID: ");
          // Batch-ID-CheckIN ausgeben
          for (byte i = 0; i < Batch.uid.size; i++)
          {
            Serial.print(Batch.uid.uidByte[i] < 0x10 ? "0" : " ");
            Serial.print(Batch.uid.uidByte[i], HEX);
          }
          Serial.print(" -> CheckIN");
          check = true;
        }
      }
    }

    // Überprüfung ob Batch-ID-CheckIN gleich Batch-ID-CheckOUT ist.
    else 
    {
      Serial.println();
      Serial.print("Gelesene UID: ");
      // Batch-ID-CheckOUT beschreiben und ausgeben
      for (int x = 0; x < 4; x++)
      {
        //Batch.uid.uidByte[x] = batch_uid_checkOUT[x];
        Serial.print(Batch.uid.uidByte[x] < 0x10 ? "0" : " ");
        Serial.print(Batch.uid.uidByte[x], HEX);
      }
      Serial.print(" -> CheckOUT");
      check = false;
    }

    
    // Halt PICC
    Batch.PICC_HaltA();
    delay(800);

    // Stop encryption on PCD
    Batch.PCD_StopCrypto1();
  }
}
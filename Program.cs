using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggen
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            // En lista som innehåller vektorer för att spara inlägg och information om dem
            List<string[]> blogList = new List<string[]>(); 

            // En bool variabel för att kontrollera om listan är sorterad eller inte
            bool listSorted = false;

            //En bool variabel för att kunna köra och avsluta meny loopen
            bool isRunning = true;

            //En while loop för att kunna skapa en meny och avsluta programmet när loopen avbryts
            while (isRunning)

            {   //Använder Console.Clear(); för att hålla konsolfönstret simpelt och rent, raderar onödig information           
                Console.Clear();

                //Presenterar meny
                Console.WriteLine(" Vällkommen till bloggen!" +
                                  "\n Välj 1-6 i menyn: " +
                                  "\n\n\t[1] Skriv nytt inlägg" +
                                  "\n\t[2] Sök inlägg" +
                                  "\n\t[3] Skriv ut alla inlägg" +
                                  "\n\t[4] Sortera inlägg" +
                                  "\n\t[5] Redigera inlägg" +
                                  "\n\t[6] Ta bort inlägg" +
                                  "\n\t[7] Avsluta programmet" );

                //Använder TryParse för att ta in användarens val och spara i en int variabel,
                //TryParse hanterar fel om man använder bokstaver istället för siffror
                Int32.TryParse(Console.ReadLine(), out int menuChoise);
               
                //En switch sats för att skapa meny, strukturen i den passar bättre för en meny än en loop 
                switch (menuChoise)
                {   
                    //case 1 = menyval 1 osv...
                    //Spara nytt inlägg
                    case 1:
                        Console.Clear();

                        //Skapar en strängvektor med 3 element i blogList listan för att spara titel, innehåll och datum
                        //En ny vektor skapas varje gång man väljer meny 1 

                        string[] blogPost = new string[3];
                        Console.WriteLine("Skriv in titel: ");

                        //Ger första elementet i vektorn värde av användarens inmatning och representerar titel
                        blogPost[0] = Console.ReadLine();                                 
                        Console.WriteLine("\nSkriv in text: ");

                        //Samma princip som i föregående exempel, representerar innehåll
                        blogPost[1] = Console.ReadLine();

                        //Här sparar jag datum och exakt tid när inlägget skapades i ett element av vektorn
                        blogPost[2] = DateTime.Now.ToString();

                        //Lägger till vektorn i blogList listan
                        blogList.Add(blogPost);
                        Console.WriteLine("\nTack! Ditt inlägg är sparat."+
                                          "\n\n\t" + blogPost[2]);

                        //Ställer om värdet av listSorted till false så programmet vet att inläggen måste sorteras igen
                        listSorted = false;

                        //En metod som skriver ut upprepande text
                        BackToMenu();

                        //Avslutar case 1 med ett break och går tillbaka till menyn
                        break;


                        //Söka efter inlägg
                    case 2:
                         Console.Clear();
                         Console.WriteLine("\nVar snäll och ange titel som du söker efter: ");

                        /*------------Variant med linjär sökning-------------
                          
                          //En variabel med värde av användarens inmatning
                          string key = Console.ReadLine();

                          // En for loop för att gå igenom alla element i listan
                            for (int i = 0; i < blogList.Count; i++)
                         {
                             //Jämför inläggets titel och användarens inmatning
                             //Använder ToLower() metoden för att eliminera skiftlägeskänslighet
                             if (blogList[i][0].ToLower() == key.ToLower())
                             {
                                    Console.Clear();
                                    Console.WriteLine("Inlägg hittades.");

                                    //En metod som tar in en lista och en "initialiser" som parameter
                                    //Skriver ut inläggets plats, titel, innehåll och datum baserat på indexplats i listan som "initialiser" bestämmer 
                                    printBlog(blogList, i);
                                    BackToMenu();

                             }

                             //Skriver ut ett meddelande ifall titel inte hittas
                             else
                             {
                                 Console.Clear();
                                 Console.WriteLine("Inlägg hittades inte. " +
                                                   "\nFörsök igen.");
                                 BackToMenu();
                             }

                         }
                         break; */


                        //-------------Variant med binär sökning------------------

                        //Kollar ifall det finns element i blogList listan
                        if (blogList.Count > 0)
                        {
                            
                            //Kollar om listan är sorterad, ifall den inte är sorterad så körs inte koden
                            if (listSorted)                            {
                            // Tar emot användarens sökning och sparar i en variabel
                            string key = Console.ReadLine();

                            // Variabel för första index i listan
                            int first = 0;

                            // Variabel för sista index i listan
                            int last = blogList.Count - 1;
                            
                            //En while loop för att gå igenom listan och jämföra
                            while (first <= last)

                            {   //Mitten mellan plats first och last
                                int mid = (first + last) / 2;

                                //Jämför användarens sökning och titel av inlägg som är i mitten av första index och sista
                                //Metoden CompareTo() returnerar positivt eller negativt tal beroende på om värden man jämför är lika eller inte
                                int compareList = key.ToLower().CompareTo(blogList[mid][0].ToLower());

                                //Går upp i listan för att leta vidare
                                if (compareList > 0)
                                {
                                    first = mid + 1;
                                }
                                //Går ner i listan för att leta vidare
                                else if (compareList < 0)
                                {
                                    last = mid - 1;
                                }

                                //Annars betyder det att algoritmen hittade titel som stämmer överens med sökordet och skriver ut ett meddelande om det 
                                else
                                {                                   
                                    Console.Clear();
                                    Console.WriteLine("Inlägg hittades.");
                                    printBlog(blogList, mid);
                                    BackToMenu();                                    
                                    break;
                                }

                            }
                            //Om sökningen misslyckades så får man upp ett meddelande om det
                            if (first > last) { 

                                Console.Clear();
                                Console.WriteLine("\nInlägg hittades inte. " +
                                              "\nFörsök igen.");
                                BackToMenu();
                            }

                               
                        }   //Meddelar att listan måste sorteras innan man kan söka
                            else 
                            {
                                Console.WriteLine("\nInlägg måste sorteras först. Välj ([4] sortera inlägg) i menyn.");
                            }
                        }
                        //Ett meddelande ifall det finns inga sparade inlägg 
                        else { 
                            Console.WriteLine("\nDet finns inga sparade inlägg.");
                        }
                        break;


                        //Skriver ut alla sparade inlägg, använder samma kod som jag redan har beskrivit
                        //Går igenom listan med en for loop och använder metoden printBlog()
                    case 3:
                        
                        Console.Clear();
                        if (blogList.Count > 0) {
                            for (int i = 0; i < blogList.Count; i++)
                            {
                                printBlog(blogList, i);
                            }
                        }
                        //Meddelar att det finns inga sparade inlägg
                        else {
                            Console.WriteLine("\nDet finns inga inlägg att skriva ut. Skapa inlägg först.");
                             }

                        BackToMenu();
                        break;

                        //Sortera inlägg
                    case 4:
                        //Använder bubbelsortering
                        Console.Clear();
                        if (blogList.Count > 0)
                        {
                            Console.WriteLine("\nInlägg är sorterade. Skriv ut för att se resultat. ");

                            //Variabel som innehåller sista elementet i listan
                            int max = blogList.Count - 1;

                            // Går igenom listan
                            for (int i = 0; i < max; i++)
                            {
                                //Går igenom varje element och räknar hur många element som inte är sorterade
                                for (int j = 0; j < (max - i); j++)
                                {
                                    // Skapar en variabel för att jämföra strängar
                                    int contain = blogList[j][0].CompareTo(blogList[j + 1][0]);

                                    if (contain > 0)
                                    {
                                        //Byter plats på element
                                        string temp = blogList[j][0];
                                        blogList[j][0] = blogList[j + 1][0];
                                        blogList[j + 1][0] = temp;

                                    }
                                }
                            }
                            // Listan är sorterad.
                            listSorted = true; 


                        }
                        //Annars får man upp meddelande om att det finns inga sparade inlägg
                        else
                        {
                            Console.WriteLine("\n\tDet saknas inlägg. Skapa inlägg först.");
                        }
                        
                        break;

                        //Redigera inlägg
                    case 5:
                        Console.Clear();
                        Console.WriteLine("\nAnge titel för inlägg du vill redigera: ");

                        //En variabel som innehåller användarens inmatning
                        string editKey = Console.ReadLine();

                        if (blogList.Count > 0)
                        {
                            for(int i = 0; i < blogList.Count; i++)
                            {
                                //Jämför användarens sökning och inläggets titel
                                if (blogList[i][0].ToLower() == editKey.ToLower())
                                {
                                    printBlog(blogList, i);
                                    Console.WriteLine("\nAnge vad du vill ändra: "
                                                    + "\n\n\t[1] Titel" +
                                                      "\n\t[2] Innehåll" +
                                                      "\n\t[3] Gå tillbaka till meny.");
                                    Int32.TryParse(Console.ReadLine(), out int editMenyChoice);

                                    //Skapar en meny där användaren kan välja att ändra inläggets titel eller innehåll 
                                    switch (editMenyChoice)
                                    {    
                                        //Titel
                                        case 1:
                                            Console.Clear();
                                            Console.WriteLine("\nAnge ny titel: ");
                                            string newTitle = Console.ReadLine();

                                            //Byter plats på inläggets titel och användarens inmatning
                                            blogList[i][0] = newTitle;

                                            //Sparar ett nytt datum för att se när inlägget var senast redigerat
                                            blogList[i][2] = DateTime.Now.ToString();
                                            Console.WriteLine("\n Titel är sparad.");

                                            //Återställer variabeln som indikerar att listan inte är sorterad
                                            listSorted = false;
                                            

                                            break;
                                        //Innehåll
                                        case 2:
                                            Console.Clear();
                                            Console.Clear();
                                            Console.WriteLine("\nAnge nytt innehåll: ");
                                            string newContent = Console.ReadLine();

                                            //Sparar användarens inmatning som nytt innehåll i inlägget
                                            blogList[i][1] = newContent;
                                            blogList[i][2] = DateTime.Now.ToString();
                                            Console.WriteLine("\n Innehållet är sparat.");
                                            break;

                                        //Gå tillbaka till huvudmenyn
                                        case 3:
                                            Console.Clear();                                            
                                            break;

                                        //Hanterar felinmatning
                                        default:
                                            Console.Clear();
                                            Console.WriteLine("\nBara siffror mellan 1-3 tillåtna.");
                                            BackToMenu();
                                            break;

                                    }

                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInlägg hittades inte. Kontollera stavning eller skapa inlägg först.");
                        }
                        BackToMenu();
                        break;


                        
                        //Ta bort inlägg
                    case 6:
                        Console.Clear();
                        Console.WriteLine("\nAnge titel för inlägg du vill ta bort: ");
                        string deleteKey = Console.ReadLine();
                        if (blogList.Count > 0) { 
                        for (int i = 0; i < blogList.Count; i++)

                        {   //Söker efter titel
                            if (blogList[i][0].ToLower() == deleteKey.ToLower())
                            {
                                Console.WriteLine("\nInlägg " + blogList[i][0] +  " är nu borttagen.");

                                //Ifall titel har hittats så tar programmet bort element från listan med hjälp av RemoveAt()
                                //Där i är indexplats av elementet i listan
                                blogList.RemoveAt(i);                                                               
                            }
                           
                        }

                            
                        }

                        else
                        {
                            Console.WriteLine("\nInlägg hittades inte. Kontollera stavning eller skapa inlägg först.");
                        }
                        BackToMenu();
                        break;


                        //Avslutar programmet
                    case 7:
                        Console.Clear();
                        Console.WriteLine("\nTack för att du använde bloggen! " +
                                          "\nTryck [ENTER] för att avsluta programmet.");

                        //Sätter variabeln isRunning till false och avlutar while loopen, då stängs hela programmet ner
                        isRunning = false;
                        break;

                       //Hanterar felinmatning
                    default:
                        Console.Clear();
                        Console.WriteLine("\nBara siffror mellan 1-6 tillåtna.");
                        BackToMenu();
                        break;
                }
                Console.ReadLine();
            }
        }


        //----------------------Metoder-------------------------//

        //En metod för text som upprepas genom hela programmet
        static void BackToMenu() 
        {
            Console.WriteLine("\nTryck [ENTER] för att återvända till menyn.");
            
        }

        
        //En metod för att kunna skriva ut inläggets innehål beroende på lista och initialiser
        static void printBlog(List<string[]> blogList, int listIndex)
        {   
           Console.WriteLine("\n--------------------------");
           Console.WriteLine("Inlägg nr " + "[" + (listIndex + 1) + "]");
           Console.WriteLine("\nTitel: " + blogList[listIndex][0]);
           Console.WriteLine("\nText: " + blogList[listIndex][1]);
           Console.WriteLine("\n\t" + blogList[listIndex][2]);
                 
        }
       

    }
}

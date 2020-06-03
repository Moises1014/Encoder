using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
           // Application.Run(new Form1());
            string wce;
            string number;
            string uc;

            //Gets user input
            Console.WriteLine("Do you want to encode or decode, please enter 1 to encode or 2 to decode");
            uc = Console.ReadLine();
            if (uc == "1")
            {
                //input
                Console.WriteLine("Please enter the characters to encode");
                wce = Console.ReadLine();

                string s = encode(wce);

                //output
                Console.WriteLine("\nYour encoded code is: ");
                Console.WriteLine("\n" + s);
                Console.WriteLine("\nPlease press any key to exit");
                Console.ReadKey();
            }//end of if statement 

            if (uc== "2")
            {
                //input
                Console.Write("Enter the number to convert(put an , to separate if more than one): ");
                number = Console.ReadLine();

                string s = decode(number);

                //output
                Console.WriteLine("\nYour decoded code is: ");
                Console.WriteLine("\n" + s);
                Console.WriteLine("\nPlease press any key to exit");
                Console.ReadKey();
            }//end of if statement

            //used to test s == decode(encoded(s)) worked
            /*if (uc == "3")
            {
                Console.WriteLine("\nYour answer is: ");
                string c = "tacocat";
                Console.WriteLine(decode(encode(c)));
                Console.ReadKey();
            }*/

        }//end of main method

        /*
         * In: an string to encode according to guidelines given
         * 
         * encodes any string given to it 
         * 
         * out: return the encoded version of the string as a string  
         */
        public static string encode(string wce)
        {
            string encoded_code = "";
            List<string> words = new List<string>();
            for (int i = 0; i < wce.Length; i += 4)
            {
                int ncl = wce.Length - i;
                string nw = "";

                if (ncl < 4)
                {
                    nw = wce.Substring(i, ncl);
                }
                else
                {
                    nw = wce.Substring(i, 4);
                }
                words.Add(nw);
            }
            foreach (string nw in words)
            {
                int[] arrdex_value = new int[4] { 0, 0, 0, 0 };
                int[] binary_code = new int[4] { 0, 0, 0, 0 };
                string[] bit = new string[4];
                int encdec;
                string newbit = "";
                Stack<int> dex_value = new Stack<int>();
                List<string> strings = new List<string>();
                    
                    /*
                      * Made to Push back the strings enter by user.
                      * 
                      */

                foreach (char c in nw)
                {
                    dex_value.Push(System.Convert.ToInt32(c));
                }//End for loop

                int num_letters = dex_value.Count;

                /*
                 *Made to move number to the rigth and add the zero paddings where needed 
                 */
                switch (num_letters)
                {
                    case 4:
                        dex_value.CopyTo(arrdex_value, 0);
                        break;
                    case 3:
                        dex_value.CopyTo(arrdex_value, 1);
                        break;
                    case 2:
                        dex_value.CopyTo(arrdex_value, 2);
                        break;
                    case 1:
                        dex_value.CopyTo(arrdex_value, 3);
                        break;
                    case 0:
                        dex_value.CopyTo(arrdex_value, 0);
                        break;
                }//end switch statement

                /*
                 * Sends the array to Convert_Hex_to_8bit method and converts 
                 * from hex to binary for each string.
                 */
                for (int x = 0; x < arrdex_value.Length; x++)
                {
                    binary_code[x] = Convert_Hex_to_8bit(arrdex_value[x]);
                }//end for loop

                /*
                 *Sends the binary of each string to Complete_binary method 
                 * to put them together and pad where ever it needs zero in 
                 * front of the strings that need it.
                 * 
                 */
                for (int x = 0; x < binary_code.Length; x++)
                {
                    bit[x] = Complete_binary(binary_code[x]);
                }//end for loop

                /*
                 * Puts the new binary code in a string since to me its easier and quicker 
                 * to check and encode it
                 */
                for (int x = 0; x < bit.Length; x++)
                {
                    newbit += bit[x];
                }//end for loop

                /*
                 * Sends the new string binary to the binary_after_endcode,
                 * and sends back and encoded binary code as a string
                 */
                string encbin = binary_after_endcode(newbit);


                /*
                 *Converts the string of the encoded binary to and dec thats encoded
                 * as an int.
                 */
                encdec = Convert.ToInt32(encbin, 2);


                /*
                 *Used to test output 
                 */
                /*Console.WriteLine(nw);
                Console.WriteLine("Binary Code:");
                Console.WriteLine(newbit);
                Console.WriteLine("Encoded Binary Code:");
                Console.WriteLine(encbin);
                Console.WriteLine("encoded dec:");
                Console.WriteLine(encdec);*/
                encoded_code += encdec + ",";
            }

            encoded_code = encoded_code.Remove(encoded_code.Length - 1, 1);
            return encoded_code;
        }
                

        /*
         *In: an int to convert from hex to binary
         * 
         * Converts any hex number to a binary code 
         * 
         * out: binary as an int  
         */
        public static int Convert_Hex_to_8bit(int cha)
        {
            int rem, i = 1, binary = 0; 
            while (cha != 0)
            {
                rem = cha % 2;
                cha /= 2;
                binary += rem * i;
                i *= 10;
            }
            return binary;
        }//end method
       
        /*
       *In: a binary code of one string
       * 
       * Converts the binary to an 8bit binary number by padding the zeros 
       * needed 
       * 
       * out: complete 8bit binary string  
       */
        public static string Complete_binary (int num)
        {
            string com_binary;
            com_binary = num.ToString("00000000");
            return com_binary;
        }//end method

       /*
       *In: complete 32 bit binary code
       * 
       * makes the binary code by checking if there is a 1 in a specific place 
       * or index and if so changes the intialized 0 to 1, if not a 1 the place
       * then the zero stays the same.
       * 
       * out: new encoded 32 bit binary code  
       */
        public static string binary_after_endcode(string code)
        {
            string newbin = "00000000000000000000000000000000";
            
            //first string
           
                if (code.Substring(0, 1) == "1")
                {
                    
                    newbin = newbin.Remove(0, 1).Insert(0, "1");

                }//end if statement
                if (code.Substring(1, 1) == "1")
                {
                 
                    newbin = newbin.Remove(4, 1).Insert(4, "1");
                }//end if statement

                 if (code.Substring(2, 1) == "1")
                {
                   
                    newbin = newbin.Remove(8, 1).Insert(8, "1");
                }//end if statement

            if (code.Substring(3, 1) == "1")
                {
                   
                    newbin = newbin.Remove(12, 1).Insert(12, "1");
                }//end if statement

                if (code.Substring(4, 1) == "1")
                {
                  
                    newbin = newbin.Remove(16, 1).Insert(16, "1");
            }//end if statement

            if (code.Substring(5, 1) == "1")
                {
                  
                    newbin = newbin.Remove(20, 1).Insert(20, "1");
            }//end if statement

            if (code.Substring(6, 1) == "1")
                {
                 
                    newbin = newbin.Remove(24, 1).Insert(24, "1");
            }//end if statement

            if (code.Substring(7, 1) == "1")
                {
                   
                    newbin = newbin.Remove(28, 1).Insert(28, "1");
            }//end if statement

/*-----------------------------------------------------------------------------------------------------------------------*/
            // second string
            if (code.Substring(8, 1) == "1")
                {
           
                    newbin = newbin.Remove(1, 1).Insert(1, "1");
            }//end if statement

            if (code.Substring(9, 1) == "1")
                {
                  
                    newbin = newbin.Remove(5, 1).Insert(5, "1");
            }//end if statement

            if (code.Substring(10, 1) == "1")
                {
                    
                    newbin = newbin.Remove(9, 1).Insert(9, "1");
            }//end if statement

            if (code.Substring(11, 1) == "1")
                {
                   
                    newbin = newbin.Remove(13, 1).Insert(13, "1");
            }//end if statement

            if (code.Substring(12, 1) == "1")
                {
            
                    newbin = newbin.Remove(17, 1).Insert(17, "1");
            }//end if statement

            if (code.Substring(13, 1) == "1")
                {
                   
                    newbin = newbin.Remove(21, 1).Insert(21, "1");
            }//end if statement

            if (code.Substring(14, 1) == "1")
                {
                 
                    newbin = newbin.Remove(25, 1).Insert(25, "1");
            }//end if statement

            if (code.Substring(15, 1) == "1")
                {
           
                    newbin = newbin.Remove(29, 1).Insert(29, "1");
            }//end if statement

/*-----------------------------------------------------------------------------------------------------------------------*/
            // third string
            if (code.Substring(16, 1) == "1")
                {
                    
                    newbin = newbin.Remove(2, 1).Insert(2, "1");
            }//end if statement

            if (code.Substring(17, 1) == "1")
                {
                   
                    newbin = newbin.Remove(6, 1).Insert(6, "1");
            }//end if statement

            if (code.Substring(18, 1) == "1")
                {
                  
                    newbin = newbin.Remove(10, 1).Insert(10, "1");
            }//end if statement

            if (code.Substring(19, 1) == "1")
                {
                   
                    newbin = newbin.Remove(14, 1).Insert(14, "1");
            }//end if statement

            if (code.Substring(20, 1) == "1")
                {
                  
                    newbin = newbin.Remove(18, 1).Insert(18, "1");
            }//end if statement

            if (code.Substring(21, 1) == "1")
                {
                 
                    newbin = newbin.Remove(22, 1).Insert(22, "1");
            }//end if statement

            if (code.Substring(22, 1) == "1")
                {
                  
                    newbin = newbin.Remove(26, 1).Insert(26, "1");
            }//end if statement

            if (code.Substring(23, 1) == "1")
                {
                    
                    newbin = newbin.Remove(30, 1).Insert(30, "1");
            }//end if statement

  /*-----------------------------------------------------------------------------------------------------------------------*/
            // fourth string
            if (code.Substring(24, 1) == "1")
                {
                   
                    newbin = newbin.Remove(3, 1).Insert(3, "1");
            }//end if statement

            if (code.Substring(25, 1) == "1")
                {
                   
                    newbin = newbin.Remove(7, 1).Insert(7, "1");
            }//end if statement

            if (code.Substring(26, 1) == "1")
                {
                   
                    newbin = newbin.Remove(11, 1).Insert(11, "1");
            }//end if statement

            if (code.Substring(27, 1) == "1")
                {
                 
                    newbin = newbin.Remove(15, 1).Insert(15, "1");
            }//end if statement

            if (code.Substring(28, 1) == "1")
                {
                
                    newbin = newbin.Remove(19, 1).Insert(19, "1");
            }//end if statement

            if (code.Substring(29, 1) == "1")
                {
                  
                    newbin = newbin.Remove(23, 1).Insert(23, "1");
            }//end if statement

            if (code.Substring(30, 1) == "1")
                {
                    
                    newbin = newbin.Remove(27, 1).Insert(27, "1");
            }//end if statement

            if (code.Substring(31, 1) == "1")
                {
                   
                    newbin = newbin.Remove(31, 1).Insert(31, "1");
            }//end if statement

            return newbin;
        }//end method


        /*
         *In: an string to be decoded
         * 
         * decodes the code that has been encoded 
         * 
         * out: string of the decoded code  
         */
        public static string decode(string number)
        {
            List<string> words = new List<string>();
            
            //code that will be returned
            string answer="";

            //split to 32 bit binary or four letters per
            words = number.Split(',').ToList();
            foreach (string c in words)
            {
                
                string decoded_bin = "00000000000000000000000000000000";
                int n;
                n = Convert.ToInt32(c);
                string result = Convert.ToString(n, 2).PadLeft(32, '0');

/***********************************************************************************************/
                //first string

                if (result.Substring(0, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(0, 1).Insert(0, "1");

                }//end if statement
                if (result.Substring(1, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(8, 1).Insert(8, "1");
                }//end if statement

                if (result.Substring(2, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(16, 1).Insert(16, "1");
                }//end if statement

                if (result.Substring(3, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(24, 1).Insert(24, "1");
                }//end if statement

                if (result.Substring(4, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(1, 1).Insert(1, "1");
                }//end if statement

                if (result.Substring(5, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(9, 1).Insert(9, "1");
                }//end if statement

                if (result.Substring(6, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(17, 1).Insert(17, "1");
                }//end if statement

                if (result.Substring(7, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(25, 1).Insert(25, "1");
                }//end if statement

  /*-----------------------------------------------------------------------------------------------------------------------*/
                // second string
                if (result.Substring(8, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(2, 1).Insert(2, "1");
                }//end if statement

                if (result.Substring(9, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(10, 1).Insert(10, "1");
                }//end if statement

                if (result.Substring(10, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(18, 1).Insert(18, "1");
                }//end if statement

                if (result.Substring(11, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(26, 1).Insert(26, "1");
                }//end if statement

                if (result.Substring(12, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(3, 1).Insert(3, "1");
                }//end if statement

                if (result.Substring(13, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(11, 1).Insert(11, "1");
                }//end if statement

                if (result.Substring(14, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(19, 1).Insert(19, "1");
                }//end if statement

                if (result.Substring(15, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(27, 1).Insert(27, "1");
                }//end if statement

  /*-----------------------------------------------------------------------------------------------------------------------*/
                // third string
                if (result.Substring(16, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(4, 1).Insert(4, "1");
                }//end if statement

                if (result.Substring(17, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(12, 1).Insert(12, "1");
                }//end if statement

                if (result.Substring(18, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(20, 1).Insert(20, "1");
                }//end if statement

                if (result.Substring(19, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(28, 1).Insert(28, "1");
                }//end if statement

                if (result.Substring(20, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(5, 1).Insert(5, "1");
                }//end if statement

                if (result.Substring(21, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(13, 1).Insert(13, "1");
                }//end if statement

                if (result.Substring(22, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(21, 1).Insert(21, "1");
                }//end if statement

                if (result.Substring(23, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(29, 1).Insert(29, "1");
                }//end if statement

 /*-----------------------------------------------------------------------------------------------------------------------*/
                // fourth string
                if (result.Substring(24, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(6, 1).Insert(6, "1");
                }//end if statement

                if (result.Substring(25, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(14, 1).Insert(14, "1");
                }//end if statement

                if (result.Substring(26, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(22, 1).Insert(22, "1");
                }//end if statement

                if (result.Substring(27, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(30, 1).Insert(30, "1");
                }//end if statement

                if (result.Substring(28, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(7, 1).Insert(7, "1");
                }//end if statement

                if (result.Substring(29, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(15, 1).Insert(15, "1");
                }//end if statement

                if (result.Substring(30, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(23, 1).Insert(23, "1");
                }//end if statement

                if (result.Substring(31, 1) == "1")
                {
                    decoded_bin = decoded_bin.Remove(31, 1).Insert(31, "1");
                }//end if statement
 /********************************************************************************************/
                
                var list = new List<Byte>();

                //turns the binaryto letters
                for (int i = 0; i < decoded_bin.Length; i += 8)
                {
                    String t = decoded_bin.Substring(i, 8);
                    list.Add(Convert.ToByte(t, 2));
                }//end of forloop

                //Puts letters in the rigth order
                list.Reverse();
                string s = Encoding.ASCII.GetString(list.ToArray());

                //adds to string to make the sentence or code together
                answer += s;

                //Used this for testing code

               /* Console.WriteLine("Binary of the given number= ");
                Console.WriteLine(result);
                Console.WriteLine("Dencodedbinary of the given number= ");
                Console.WriteLine(decoded_bin);
                Console.WriteLine(s);*/
            }//end of foreach loop

            //return new decoded code to main
            return answer;
        }//end of decoded method

    }//end of class program
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher
{
    class Vigenere
    {
        private char[] alphabet;


        public Vigenere()
        {
            alphabet = new[] {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н',
                'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        }

        public string Encrypt(string message, string code)
        {

            int messageLength = message.Length;

            char[] codeFull = Extension(messageLength,code);

            char[] encryptedString= new char[messageLength];

            for (int i = 0; i < messageLength; i++)
            {
                char k = message[1];

                int letterInd = Array.IndexOf(alphabet, message[i]);
                int codeInd = Array.IndexOf(alphabet, codeFull[i]);

                encryptedString[i] = Convert.ToChar(alphabet[(letterInd + codeInd) % alphabet.Length]);
            }


            return new string(encryptedString);
        }

        public string Decrypt(string message, string code)
        {

            int messageLength = message.Length;

            char[] codeFull = Extension(messageLength, code);

            char[] encryptedString = new char[messageLength];

            for (int i = 0; i < messageLength; i++)
            {
                char k = message[1];

                int letterInd = Array.IndexOf(alphabet, message[i]);
                int codeInd = Array.IndexOf(alphabet, codeFull[i]);

                encryptedString[i] = Convert.ToChar(alphabet[(letterInd - codeInd+ alphabet.Length) % alphabet.Length]);
            }


            return new string(encryptedString);
        }

        char[] Extension(int messageLength, string code)
        {
            char[] codeFull = new char[messageLength];

            for (int i = 0, j = 0; i < codeFull.Length; i++, j++)
            {
                if (j == code.Length)
                {
                    j = 0;
                }


                codeFull[i] = code[j];
            }

            return codeFull;
        }
    }
}

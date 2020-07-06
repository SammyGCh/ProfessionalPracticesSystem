/*
        Date: 05/06/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System;
using System.Text;
using System.Security.Cryptography;

namespace BusinessLogic
{
    public class HashManagement
    {
        byte[] transformSource;
        byte[] transformHash;
        

        public HashManagement()
        {
            transformSource = null;
            transformHash = null;
        }

        public String TextToHash(string sourceData)
        {
            MD5 md5Code = MD5.Create();
            transformSource = Encoding.ASCII.GetBytes(sourceData);
            transformHash = md5Code.ComputeHash(transformSource);

            StringBuilder hashChain = new StringBuilder();

            foreach (var letter in transformHash)
            {
                hashChain.Append(letter.ToString("X2"));
            }

            return hashChain.ToString();
        }

        public bool CompareHashs(string hashedStringRecieved, string hashedStringOriginal)
        {
            bool isEqual = false;

            if (hashedStringRecieved == hashedStringOriginal)
            {
                isEqual = true;
            }

            return isEqual;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseOneCodingChallenge
{
    class MaskingFunction
    {
        public static string FormatDeterminer(string infoString)
        {
            var netInfoString = infoString.Trim();
            var firstCharacter = netInfoString[..1];
            //calls on the sample input
            string newInfoString;
            switch (firstCharacter)
            {
                
                case "[":
                    newInfoString = InputType1(netInfoString);
                    break;
                default:
                    newInfoString = "cannot hide sensitive information";
                    break;
            }

            return newInfoString;
        }

        //splits the string into separate entities
        private static string InputType1(string netInfoString)
        {
            char[] delimiterChars = { '\r', '\n' };
            var infoSplit = netInfoString.Split(delimiterChars);
            var newDic = new Dictionary<string, string>();
            foreach (var rowSplit in infoSplit)
            {
                if (rowSplit.Trim() != string.Empty)
                {
                    newDic.Add(rowSplit.Trim().Replace("[", "").Split(']').First(),
                        rowSplit.Trim().Split('>').Last().Trim());
                    
                }
            }

            string outputString = string.Empty;
            foreach (var a in newDic)
            {
                
                //the exclusion of the specific entities that needed masking
                var numString = a.Value;
                switch (a.Key)
                {
                    case "cardNumber":
                        numString = ReplaceInfo(a.Value);
                        break;
                    case "cardExpiry":
                        numString = ReplaceInfo(a.Value);
                        break;
                    case "cardCVV":
                        numString = ReplaceInfo(a.Value);
                        break;
                   
                }

                outputString += "[" + a.Key + "] => " + numString + "\r\n";
            }

            
            outputString = outputString[..^2];

            return outputString;
        }
        //replaces the sensitive information with *
        private static string ReplaceInfo(string rep)
        {
            string replaceInfo = string.Empty;
            for (int i = 0; i < rep.Length; i++)
            {
                replaceInfo += "*";
            }

            return replaceInfo;
        }
    }
}



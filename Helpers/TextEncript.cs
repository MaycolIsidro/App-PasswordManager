namespace PasswordManager.Helpers
{
    public class TextEncript
    {
        private readonly Dictionary<char, string> ValuesEncript;
        private readonly Dictionary<string, char> ValuesText;
        public TextEncript()
        {
            ValuesEncript = new Dictionary<char, string>()
            {
                { 'a', "{]5.y=9" },
                { 'b', "Gdf+h8*" },
                { 'c', "hyTfht5" },
                { 'd', ".-+7ju?" },
                { 'e', "89-phy/" },
                { 'f', "-*LvT66" },
                { 'g', "=(J7=)}" },
                { 'h', "23!e2#)" },
                { 'i', "*__+#+)" },
                { 'j', "{]g--]-" },
                { 'k', "*¿8=¿)P" },
                { 'l', "7)Ad28&" },
                { 'm', "-%j8u73" },
                { 'n', "l&/r-.5" },
                { 'ñ', "<>{)=8j" },
                { 'o', "x8Hfy6-" },
                { 'p', "+*)1ds|" },
                { 'q', "jd8/TH7" },
                { 'r', "5he5.y9" },
                { 's', "-}tfh]{" },
                { 't', "678l9j." },
                { 'u', "=4.)K69" },
                { 'v', "¿8._{H]" },
                { 'w', "/-5H%$#" },
                { 'x', "$rgHJt)" },
                { 'y', "!x|¿AW=" },
                { 'z', "0-#VC)(" },
                { 'A', "5z&4HhG" },
                { 'B', "sW*-LZ#" },
                { 'C', "G*-$1)1" },
                { 'D', "-F+V_.]" },
                { 'E', ",}.{|6)" },
                { 'F', "*.u0S,-" },
                { 'G', "+}6kH[]" },
                { 'H', "[_6_f4]" },
                { 'I', "|J-s0T8" },
                { 'J', "1*8JHT}" },
                { 'K', "lF+8**Y" },
                { 'L', "-{L-P=|" },
                { 'M', "||5+)H[" },
                { 'N', "gK*11I=" },
                { 'Ñ', "thF9{-u" },
                { 'O', "ik.-,}p" },
                { 'P', "d1r2{+4" },
                { 'Q', "45J-..t" },
                { 'R', "gt/+#J6" },
                { 'S', "7-*8./G" },
                { 'T', "+--%34t" },
                { 'U', "/-gh6(j" },
                { 'V', "g-98fg/" },
                { 'W', "2dv503*" },
                { 'X', "thdHv6-" },
                { 'Y', "zx.8-/2" },
                { 'Z', "1*-K+qS" },
                { '1', "|4}--{g" },
                { '2', "!#-yh7f" },
                { '3', "#/+T-*R" },
                { '4', "sw4.gt9" },
                { '5', "6,h=.-+" },
                { '6', "HP|=.0)" },
                { '7', ".-L-Yl(" },
                { '8', ":,4g..=" },
                { '9', "FR.7H3/" },
                { '0', "[}-$&-," },
                { '@', "gHU./*7" },
                { '|', "3LnlñEF" },
                { '!', ".|kHYj7" },
                { '#', "+#Y-*.5" },
                { '$', "-+//5rf" },
                { '%', "*5t5h/-" },
                { '&', "Th&!H.y" },
                { '/', ".LS)8l(" },
                { '(', "{(HT=9)" },
                { ')', "[.--}pl" },
                { '=', "R-[.dc}" },
                { '¿', "(9}sdrt" },
                { '?', "-J8{+ef" },
                { '{', "g(eHt4." },
                { '}', "T}#7-8j" },
                { '[', "0=we44)" },
                { ']', "10gRh7)" },
                { '+', "g-sx74[" },
                { '.', "{)GT6L]" },
                { ',', "+*+4-]-" },
                { ':', "+.L+f]," },
                { '-', "-/L{-_h" },
                { '_', "_P+}X_]" },
                { '*', "hf+47T)" },
                { '^', "-pl89=)" },
                { '~', "^8HJ{-^" },
                { '<', "lp25/.=" },
                { '>', "4$%.--H" },
                { ';', "<=)/gh5" },
            };
            ValuesText = new Dictionary<string, char>()
            {
                { "{]5.y=9", 'a' },
                { "Gdf+h8*", 'b' },
                { "hyTfht5", 'c' },
                { ".-+7ju?", 'd' },
                { "89-phy/", 'e' },
                { "-*LvT66", 'f' },
                { "=(J7=)}", 'g' },
                { "23!e2#)", 'h' },
                { "*__+#+)", 'i' },
                { "{]g--]-", 'j' },
                { "*¿8=¿)P", 'k' },
                { "7)Ad28&", 'l' },
                { "-%j8u73", 'm' },
                { "l&/r-.5", 'n' },
                { "<>{)=8j", 'ñ' },
                { "x8Hfy6-", 'o' },
                { "+*)1ds|", 'p' },
                { "jd8/TH7", 'q' },
                { "5he5.y9", 'r' },
                { "-}tfh]{", 's' },
                { "678l9j.", 't' },
                { "=4.)K69", 'u' },
                { "¿8._{H]", 'v' },
                { "/-5H%$#", 'w' },
                { "$rgHJt)", 'x' },
                { "!x|¿AW=", 'y' },
                { "0-#VC)(", 'z' },
                { "5z&4HhG", 'A' },
                { "sW*-LZ#", 'B' },
                { "G*-$1)1", 'C' },
                { "-F+V_.]", 'D' },
                { ",}.{|6)", 'E' },
                { "*.u0S,-", 'F' },
                { "+}6kH[]", 'G' },
                { "[_6_f4]", 'H' },
                { "|J-s0T8", 'I' },
                { "1*8JHT}", 'J' },
                { "lF+8**Y", 'K' },
                { "-{L-P=|", 'L' },
                { "||5+)H[", 'M' },
                { "gK*11I=", 'N' },
                { "thF9{-u", 'Ñ' },
                { "ik.-,}p", 'O' },
                { "d1r2{+4", 'P' },
                { "45J-..t", 'Q' },
                { "gt/+#J6", 'R' },
                { "7-*8./G", 'S' },
                { "+--%34t", 'T' },
                { "/-gh6(j", 'U' },
                { "g-98fg/", 'V' },
                { "2dv503*", 'W' },
                { "thdHv6-", 'X' },
                { "zx.8-/2", 'Y' },
                { "1*-K+qS", 'Z' },
                { "|4}--{g", '1' },
                { "!#-yh7f", '2' },
                { "#/+T-*R", '3' },
                { "sw4.gt9", '4' },
                { "6,h=.-+", '5' },
                { "HP|=.0)", '6' },
                { ".-L-Yl(", '7' },
                { ":,4g..=", '8' },
                { "FR.7H3/", '9' },
                { "[}-$&-,", '0' },
                { "gHU./*7", '@' },
                { "3LnlñEF", '|' },
                { ".|kHYj7", '!' },
                { "+#Y-*.5", '#' },
                { "-+//5rf", '$' },
                { "*5t5h/-", '%' },
                { "Th&!H.y", '&' },
                { ".LS)8l(", '/' },
                { "{(HT=9)", '(' },
                { "[.--}pl", ')' },
                { "R-[.dc}", '=' },
                { "(9}sdrt", '¿' },
                { "-J8{+ef", '?' },
                { "g(eHt4.", '{' },
                { "T}#7-8j", '}' },
                { "0=we44)", '[' },
                { "10gRh7)", ']' },
                { "g-sx74[", '+' },
                { "{)GT6L]", '.' },
                { "+*+4-]-", ',' },
                { "+.L+f],", ':' },
                { "-/L{-_h", '-' },
                { "_P+}X_]", '_' },
                { "hf+47T)", '*' },
                { "-pl89=)", '^' },
                { "^8HJ{-^", '~' },
                { "lp25/.=", '<' },
                { "4$%.--H", '>' },
                { "<=)/gh5", ';' },
            };
        }
        public string EncriptPassword(string password)
        {
            string passwordEncript = "";
            foreach (var item in password)
            {
                ValuesEncript.TryGetValue(item, out var value);
                value ??= "k7G.{#%";
                passwordEncript += value;
            }
            return passwordEncript;
        }
        public string DesencriptPassword(string passwordEncript)
        {
            string password = "";
            for (int i = 0; i < passwordEncript.Length; i += 7)
            {
                ValuesText.TryGetValue(passwordEncript.Substring(i,7), out var value);
                password += value;
            }
            return password;
        }
        public string GeneratePassword()
        {
            Random random = new Random();
            string numbers = "0123456789";
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string lettersUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specials = ".-,@/*+<>_;:{}[]?=#&%()|°";
            string password = "";
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(0, 10);
                password += numbers[index];
                index = random.Next(0, 26);
                password += letters[index];
                password += lettersUpper[index];
                index = random.Next(0, 25);
                password += specials[index];
            }
            return password;
        }
    }
}

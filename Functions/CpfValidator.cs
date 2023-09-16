namespace APIPoke.Functions
{
    public static class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            // Remove qualquer caractere não numérico do CPF
            cpf = new string(cpf.ToCharArray().Where(char.IsDigit).ToArray());

            // Verifica se o CPF tem 11 dígitos
            if (cpf.Length != 11)
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            // Verifica se os dígitos verificadores são iguais aos dígitos originais
            return (int.Parse(cpf[9].ToString()) == digito1) && (int.Parse(cpf[10].ToString()) == digito2);
        }
        public static string CpfClean(string cpf)
        {
            cpf = new string(cpf.ToCharArray().Where(char.IsDigit).ToArray());
            return cpf;
        }
    }
}

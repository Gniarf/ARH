namespace EasterApi;

public class Computer
{
    /// <summary>
    /// algo en 10 étapes de division entière pour calculer le jour de Pâques, trouvé dans un tableur excel en ligne par d'anciens collègues, reproduit ici en C#
    /// oui c'est horrible à lire, l'algo de calcul du jour de Pâques est horrible, cf https://fr.wikipedia.org/wiki/Calcul_de_la_date_de_P%C3%A2ques
    /// Bon courage.
    /// </summary>
    /// <param name="year">l'année pour laquelle on souhaite calculer le jour de Pâques</param>
    /// <returns>La date du jour du Pâques calculée</returns>
    public static DateTime ComputeEaster(int year)
    {
        int dd1, dv1, quotient1, reste1;
        int dd2, dv2, quotient2, reste2;
        int dd3, dv3, quotient3, reste3;
        int dd4, dv4, quotient4, reste4;
        int dd5, dv5, quotient5, reste5;
        int dd6, dv6, quotient6, reste6;
        int dd7, dv7, quotient7, reste7;
        int dd8, dv8, quotient8, reste8;
        int dd9, dv9, quotient9, reste9;
        int dd10, dv10, quotient10, reste10;
        dd1 = year;
        dv1 = 19;
        Divise(dd1, dv1, out quotient1, out reste1);
        dd2 = dd1;
        dv2 = 100;
        Divise(dd2, dv2, out quotient2, out reste2);
        dd3 = quotient2;
        dv3 = 4;
        Divise(dd3, dv3, out quotient3, out reste3);
        dd4 = quotient2 + 8;
        dv4 = 25;
        Divise(dd4, dv4, out quotient4, out reste4);
        dd5 = quotient2 - quotient4 + 1;
        dv5 = 3;
        Divise(dd5, dv5, out quotient5, out reste5);
        dd6 = 19 * reste1 + quotient2 - quotient3 - quotient5 + 15;
        dv6 = 30;
        Divise(dd6, dv6, out quotient6, out reste6);
        dd7 = reste2;
        dv7 = 4;
        Divise(dd7, dv7, out quotient7, out reste7);
        dd8 = 32 + 2 * reste3 + 2 * quotient7 - reste6 - reste7;
        dv8 = 7;
        Divise(dd8, dv8, out quotient8, out reste8);
        dd9 = reste1 + 11 * reste6 + 22 * reste8;
        dv9 = 451;
        Divise(dd9, dv9, out quotient9, out reste9);
        dd10 = reste6 + reste8 - 7 * quotient9 + 114;
        dv10 = 31;
        Divise(dd10, dv10, out quotient10, out reste10);
        return new DateTime(year, quotient10, reste10 + 1);
    }

    public static DateTime ComputePentecote(int year)
    {
        return ComputeEaster(year).AddDays(50);
    }

    public static DateTime ComputeAscension(int year)
    {
        return ComputeEaster(year).AddDays(39);
    }

    private static void Divise(int a, int b, out int quotient, out int reste)
    {
        quotient = a / b;
        reste = a - b * quotient;
    }
}
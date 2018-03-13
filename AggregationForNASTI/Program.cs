using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregationForNASTI
{
    class Program
    {
        int Stage1_Sum_bestandwhg;
        int strategy_Count = 0;

        

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.GetSum_Stage1();
            pr.CheckDifferentCount_Stage2(false);
        }

        bool CheckHundredPercentStage3(int columnCount)
        {
            var a = DataBaseController.GetDataFromDatabase("SELECT * FROM T_STRAT_WGTS Where Date=(SELECT MAX(Date) FROM T_STRAT_WGTS)", "VIVACE", columnCount + 1)[0];
            a.RemoveAt(0);
            var t_Strat_Wgts_Values_Sum = a.Sum(x => Double.Parse(x));
            if (t_Strat_Wgts_Values_Sum > 0.9995 && t_Strat_Wgts_Values_Sum<1.0005)
            {

            }
            return false;
        }

        void CheckDifferentCount_Stage2(bool ignoreDifferent)
        {
            var count = Int32.Parse(DataBaseController.SelectFromTable("Nr_Strats", "T_PARAMETER", "Strat_update=(SELECT MAX(Strat_update) FROM T_PARAMETER)", "VIVACE")[0][0]);
            if (count != strategy_Count && !ignoreDifferent)
            {
                //throw warning and waiting user.
            } else
            {
                CheckHundredPercentStage3(count);
            }

        }

        void GetSum_Stage1()//complete
        {
            var strategyData = Math.Round(Double.Parse(DataBaseController.SelectFromTable("sum(wert_fondswhg) ", "T_PERFORM", "verm_hauptkategorie ='Bankguthaben' and bewertungsdatum=(SELECT MAX(bewertungsdatum) FROM T_PERFORM)", "AXXION")[0][0]),2);
            Console.WriteLine(strategy_Count);
            Console.WriteLine(Stage1_Sum_bestandwhg);

        }


    }
}

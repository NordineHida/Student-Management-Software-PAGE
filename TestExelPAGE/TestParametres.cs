using PAGE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPAGE
{
    public class TestParametres
    {
        /*[Fact]
        public void TestChangerLangue()
        {

            Parametre.Instance.Langue = LANGUE.ANGLAIS;

            Parametre.Instance.Langue= (LANGUE.FRANCAIS);

            Assert.Equal(LANGUE.FRANCAIS, Parametre.Instance.Langue);
        }*/

        [Fact]
        public void TestSingletonInstance()
        {
            Parametre parametre1 = Parametre.Instance;

            Parametre parametre2 = Parametre.Instance;

            Assert.Equal(parametre1, parametre2);
        }
    }
}

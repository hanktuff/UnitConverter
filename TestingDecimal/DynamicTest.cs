using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDecimal {

    class DynamicTest {

        void DoSomething() {

            dynamic w = new Worker();

            int i = w.GetNumber("str");
        }
    }



    class Worker {

        public int GetNumber() {
            return DateTime.Now.Second;
        }
    }

}

﻿using ObjectRequestBrokerCS.vson;
using System;

namespace ObjectRequestBrokerCS.tests
{

    class Test : Test2
    {
        public Test()
        {
        }

        private int val;
        private String str;

        public void print()
        {
            base.print();
            System.Diagnostics.Debug.WriteLine(" val: " + val + " str: " + str);
        }
    }

    class Test2
    {
        public Test2() { }

        private double ddval;

        public void print()
        {
            System.Diagnostics.Debug.Write("ddval: " + ddval);
        }

    }

    class VsonTest
    {

        public static void test()
        {
            Test t = (Test)Vson.fromJson("{\n" +
                "\"type\": \"ObjectRequestBrokerCS.tests.Test\",\n" +
                "\"properties\": {\n" +
                "\"@hash\": 2125039532, \n" +
                "\"val\": 2,\n" +
                "\"ddval\": 25.5,\n" +
                "\"str\": \"smth\"\n" +
                "}\n" +
                "}");
            Test t2 = (Test)Vson.fromJson(Vson.toJson(t));
            t2.print();

        }

    }
}

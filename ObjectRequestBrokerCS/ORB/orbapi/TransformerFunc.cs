using System;
using ORB.requestreplyapi.common;

namespace ORB.orbapi
{
    public class TransformerFunc : IByteStreamTransformer
    {
        private Func<byte[],byte[]> _func;
        public TransformerFunc(Func<byte[],byte[]> func)
        {
            this._func = func;
        }

        byte[] IByteStreamTransformer.Transform(byte[] @in)
        {
            return this._func(@in);
        }
        
    }
}
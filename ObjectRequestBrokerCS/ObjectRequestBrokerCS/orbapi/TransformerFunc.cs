using System;
using requestreplyapi.common;

namespace orbapi
{
    public class TransformerFunc : ByteStreamTransformer
    {
        private Func<byte[],byte[]> func;
        public TransformerFunc(Func<byte[],byte[]> func)
        {
            this.func = func;
        }

        byte[] ByteStreamTransformer.transform(byte[] @in)
        {
            return this.func(@in);
        }
        
    }
}
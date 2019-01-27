using System;
using System.Diagnostics;

namespace RokuDotNet.Client.Input
{
    public class OneOf<T1, T2>
        where T1: struct
        where T2: struct
    {
        public static implicit operator OneOf<T1, T2>(T1 key)
        {
            return new OneOf<T1, T2>(key);
        }

        public static implicit operator OneOf<T1, T2>(T2 key)
        {
            return new OneOf<T1, T2>(key);
        }

        private readonly T1? t1;
        private readonly T2? t2;

        public OneOf(T1 t1)
        {
            this.t1 = t1;
        }

        public OneOf(T2 t2)
        {
            this.t2 = t2;
        }

        public void Match(Action<T1> onT1, Action<T2> onT2)
        {
            if (t1.HasValue)
            {
                onT1(this.t1.Value);
            }
            else if (t2.HasValue)
            {
                onT2(this.t2.Value);
            }
            else
            {
                Debug.Fail("One of T1 or T2 should always have a value.");
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> onT1, Func<T2, TResult> onT2)
        {
            if (t1.HasValue)
            {
                return onT1(this.t1.Value);
            }
            else if (t2.HasValue)
            {
                return onT2(this.t2.Value);
            }
            else
            {
                Debug.Fail("One of T1 or T2 should always have a value.");

                return default(TResult);
            }
        }
    }
}
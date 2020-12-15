using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    // Interface: contract
    // T: eender welk type (Generics)
    public interface IManager<T>
    {
        IReadOnlyList<T> HaalOp();
        IReadOnlyList<T> HaalOp(Func<T, bool> predicate);
        void VoegToe(T anItem);
        void Verwijder(T anItem);
        T HaalOp(long id);
        /* int:	    -2,147,483,648 to 2,147,483,647	(signed 32-bit integer) DB: int
           long:	-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(signed 64-bit integer) DB: bigint
         */
    }
}


using Forum._3.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Forum._3.Models
{
    public class PostComparer : IComparer<GetPostViewModel>
    {
        public int Compare(GetPostViewModel? p1, GetPostViewModel? p2)
        {
            if (p1 is null || p2 is null)
                throw new ArgumentException("Некорректное значение параметра");
            return p1.DateCreation.CompareTo(p2.DateCreation);
        }
    }
}

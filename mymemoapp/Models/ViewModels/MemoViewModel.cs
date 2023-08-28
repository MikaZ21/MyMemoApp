using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mymemoapp.Models.ViewModels
{
    public class MemoViewModel
    {
        public List<MyMemo> MemoList { get; set; }

        public MyMemo Memo { get; set; }
    }
}
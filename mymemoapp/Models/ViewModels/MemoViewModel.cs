using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mymemoapp.Models.ViewModels
{
    public class MemoViewModel
    {
        public List<MyMemo> MemoList { get; set; }


        [MaxLength(3000)]
        public MyMemo Memo { get; set; }
    }
}
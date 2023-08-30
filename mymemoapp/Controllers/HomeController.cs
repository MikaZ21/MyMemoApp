using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using mymemoapp.Models;
using mymemoapp.Models.ViewModels;

namespace mymemoapp.Controllers
{
      public class HomeController : Controller
      {
         private readonly ILogger<HomeController> _logger;

         public HomeController(ILogger<HomeController> logger)
         {
            _logger = logger;
         }

        public IActionResult Index()
        {
            var memoListViewModel = GetAllMemos();
            return View(memoListViewModel);
        }

        [HttpGet]
        public JsonResult PopulateForm(int id)
        {
            var memo = GetById(id);
            return Json(memo);
        }

        internal MyMemo GetById(int id)
        {
            MyMemo memo = new();

            using (var connection = 
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM mymemo WHERE Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            memo.Id = reader.GetInt32(0);
                            memo.Name = reader.GetString(1);
                        }
                        else
                        {
                            return memo;
                        }
                    };
                }
            }
            return memo;
        }




        internal MemoViewModel GetAllMemos()
        {
            List<MyMemo> memoList = new();

            using (SqliteConnection con = 
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = "SELECT * FROM mymemo";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                memoList.Add(
                                    new MyMemo
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1)
                                    });
                            }
                        }
                        else
                        {
                            return new MemoViewModel
                            {
                                MemoList = memoList
                            };
                        
                        }
                    };
                }
            }
            return new MemoViewModel
            {
                MemoList = memoList
            };
        }

        public RedirectResult Insert(MyMemo memo)
        {
            using (SqliteConnection con = 
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT INTO mymemo (name) VALUES ('{memo.Name}')";
                    try
                    {
                    tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return Redirect("https://localhost:7079/");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (SqliteConnection con = 
                new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from mymemo WHERE Id = '{id}'";
                    tableCmd.ExecuteNonQuery();
                }
            }
            return Json(new object{});
        }

        public RedirectResult Update(MyMemo memo)
        {
                using (SqliteConnection con = 
                    new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"UPDATE mymemo SET name = '{memo.Name}' WHERE Id = '{memo.Id}'";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch ( Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return Redirect("https://localhost:7079/");

        }
    }
}
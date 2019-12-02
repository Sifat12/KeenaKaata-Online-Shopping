using KeenaKaata.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using KeenaKaata.DAL;

namespace KeenaKaata.Controllers
{
    public class HomeController : Controller
    {
        dbKeenaKaataOnlineShoppingEntities context = new dbKeenaKaataOnlineShoppingEntities();
        public ActionResult Index(string search,int? page) 
        {
            HomeIndexViewModel model = new HomeIndexViewModel();

            return View(model.CreateModel(search,20,page));
        }
        public ActionResult BrandProduct(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();

            return View(model.CreateModel(search, 20, page));
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetail()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = context.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = context.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        break;
                    }
                   
                }
                Session["cart"] = cart;

            }
            return Redirect("Index");
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = context.Tbl_Product.Find(productId);
            foreach(var item in cart)
            {
                if(item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            
            Session["cart"] = cart;
            return Redirect("Index");
        }
    }
}
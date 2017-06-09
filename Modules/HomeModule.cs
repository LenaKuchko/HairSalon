using Nancy;
using HairSalon.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("listStylists", Stylist.GetAll());
        model.Add("show-info", null);
        return View["index.cshtml", model];
      };
      Get["/stylists/new"] = _ => {
        string formType = "stylist";
        return View["form.cshtml", "stylist"];
      };
      Post["/stylists/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Stylist stylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-rating"]);
        stylist.Save();
        model.Add("listStylists", Stylist.GetAll());
        model.Add("newStylist", stylist);
        model.Add("show-info", Request.Form["show-info-new"]);
        return View["index.cshtml", model];
      };
      Get["/stylists/{id}/info"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("foundedStylist", Stylist.Find(parameters.id));
        model.Add("show-info", Request.Query["show-info"]);
        model.Add("listStylists", Stylist.GetAll());
        // model.Add("stylistClients", Stylist.GetClients());
        return View["index.cshtml", model];
      };
    }
  }
}

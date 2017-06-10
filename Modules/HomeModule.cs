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
        model.Add("listClients", Client.GetAll());
        model.Add("show-info", null);
        return View["index.cshtml", model];
      };
      Get["/stylists/new"] = _ => {
        Dictionary<string, string> model = new Dictionary<string, string>{};
        model.Add("form-type", "new-stylist");
        return View["form.cshtml", model];
      };
      Post["/stylists/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Stylist stylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-rating"]);
        stylist.Save();
        model.Add("listClients", Client.GetAll());
        model.Add("listStylists", Stylist.GetAll());
        model.Add("newStylist", stylist);
        model.Add("show-info", Request.Form["show-info-new"]);
        return View["index.cshtml", model];
      };
      Get["/stylists/{id}/info"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Stylist foundedStylist = Stylist.Find(parameters.id);
        model.Add("listClients", Client.GetAll());
        model.Add("foundedStylist", foundedStylist);
        model.Add("show-info", "show-stylist");
        model.Add("listStylists", Stylist.GetAll());
        model.Add("stylistClients", foundedStylist.GetClients());
        return View["index.cshtml", model];
      };
      Get["/stylist/{id}/update"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Stylist foundedStylist = Stylist.Find(parameters.id);
        model.Add("foundedStylist", foundedStylist);
        model.Add("form-type", "update-stylist");
        // model.Add("listStylists", Stylist.GetAll());
        model.Add("stylistClients", foundedStylist.GetClients());
        return View["form.cshtml", model];
      };
      Patch["/stylists/{id}/update"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Stylist foundedStylist = Stylist.Find(parameters.id);
        foundedStylist.Update(Request.Form["stylist-name"], Request.Form["stylist-rating"]);
        model.Add("updatedStylist", foundedStylist);
        model.Add("listClients", Client.GetAll());
        model.Add("listStylists", Stylist.GetAll());
        model.Add("stylistClients", foundedStylist.GetClients());
        model.Add("show-info", "update-stylist");
        return View["index.cshtml", model];
      };
      Get["/client/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("form-type", "new-client");
        model.Add("listStylists", Stylist.GetAll());
        // model.Add("stylistClients", foundedStylist.GetClients());
        return View["form.cshtml", model];
      };
      Post["/client/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Client client = new Client(Request.Form["client-name"], Request.Form["stylists"]);
        client.Save();
        model.Add("clientStylist", Stylist.Find(client.GetStylistId()));
        model.Add("newClient", client);
        model.Add("listClients", Client.GetAll());
        model.Add("show-info", "client-new");
        model.Add("listStylists", Stylist.GetAll());
        return View["index.cshtml", model];
      };
      Get["/clients/{id}/info"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Client foundedClient = Client.Find(parameters.id);
        Stylist foundedStylist = Stylist.Find(foundedClient.GetStylistId());
        model.Add("listStylists", Stylist.GetAll());
        model.Add("foundedClient", foundedClient);
        model.Add("foundedStylist", foundedStylist);
        model.Add("show-info", "show-client");
        model.Add("listClients", Client.GetAll());
        return View["index.cshtml", model];
      };
      Get["/clients/{id}/update"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Client foundedClient = Client.Find(parameters.id);
        model.Add("foundedClient", foundedClient);
        model.Add("form-type", "update-client");
        model.Add("listStylists", Stylist.GetAll());
        // model.Add("stylistClients", foundedStylist.GetClients());
        return View["form.cshtml", model];
      };
      Patch["/client/{id}/update"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Stylist foundedClient = Client.Find(parameters.id);
        foundedClient.Update(Request.Form["client-name"], Request.Form["client-rating"]);
        model.Add("updatedClient", foundedClient);
        model.Add("listClients", Client.GetAll());
        model.Add("listStylists", Stylist.GetAll());
        // model.Add("stylistClients", foundedStylist.GetClients());
        model.Add("show-info", "update-client");
        return View["index.cshtml", model];
      };
    }
  }
}

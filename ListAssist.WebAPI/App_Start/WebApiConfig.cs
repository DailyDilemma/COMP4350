using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Owin.Security.OAuth;

namespace ListAssist.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi2",
            //    routeTemplate: "api/ListItems/{itemId}/list/{listId}",
            //    defaults: new { itemId = RouteParameter.Optional, listId = RouteParameter.Optional }
            //);

            // List Routes
            config.Routes.MapHttpRoute(
                name: "GET_AllLists",
                routeTemplate: "api/Lists",
                defaults: new { controller="Lists", action="AllLists" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) }
            );

            config.Routes.MapHttpRoute(
                name: "POST_AddList",
                routeTemplate: "api/Lists/{listName}",
                defaults: new { listName = RouteParameter.Optional, controller = "Lists", action = "AddList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "POST" }) }
            );

            config.Routes.MapHttpRoute(
                name: "DELETE_RemoveList",
                routeTemplate: "api/Lists/{listId}",
                defaults: new { listId = RouteParameter.Optional, controller = "Lists", action = "RemoveList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "DELETE" }) }
            );

            config.Routes.MapHttpRoute(
                name: "GET_SingleList",
                routeTemplate: "api/Lists/{listId}",
                defaults: new { listId = RouteParameter.Optional, controller = "Lists", action = "SingleList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) }
            );

            config.Routes.MapHttpRoute(
                name: "PUT_UpdateList",
                routeTemplate: "api/Lists",
                defaults: new { controller = "Lists", action = "UpdateList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "PUT" }) }
            );

            // ListItem Routes
            config.Routes.MapHttpRoute(
                name: "POST_AddItemToList",
                routeTemplate: "api/ListItems",
                defaults: new { controller = "ListItems", action = "AddItemToList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "POST" }) }
            );

            config.Routes.MapHttpRoute(
                name: "GET_GetItemFromList",
                routeTemplate: "api/ListItems/list/{listId}/item/{itemId}",
                defaults: new { listId = RouteParameter.Optional, itemId = RouteParameter.Optional, controller = "ListItems", action = "GetItemFromList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) }
            );

            config.Routes.MapHttpRoute(
                name: "PUT_CheckOffItemFromList",
                routeTemplate: "api/ListItems/list/{listId}/item/{itemId}",
                defaults: new { listId = RouteParameter.Optional, itemId = RouteParameter.Optional, controller = "ListItems", action = "CheckOffItemFromList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "PUT" }) }
            );

            config.Routes.MapHttpRoute(
                name: "DELETE_DeleteItemFromList",
                routeTemplate: "api/ListItems/list/{listId}/item/{itemId}",
                defaults: new { listId = RouteParameter.Optional, itemId = RouteParameter.Optional, controller = "ListItems", action = "DeleteItemFromList" },
                constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "DELETE" }) }
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi1",
            //    routeTemplate: "api/ListItems/list/{listId}/listItem/{itemId}",
            //    defaults: new { listId = RouteParameter.Optional, itemId = RouteParameter.Optional, controller = "ListItems" },
            //    constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "DELETE", "POST", "PUT" }) }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi2",
            //    routeTemplate: "api/ListItems",
            //    defaults: new { controller = "ListItems" },
            //    constraints: new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi3",
            //    routeTemplate: "api/Lists/{listId}",
            //    defaults: new { listId = RouteParameter.Optional, controller="Lists" }
            //);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class HomeModel
    {

        private const int LIST_ITEMS_QUANTITY = 5;

        public string CellName { get; set; }

        public string Description { get; set; }

        public HomeListModel[] Lists { get; set; }

        public HomeModel()
        {
            //
        }

        public HomeModel(Cell item)
        {
            this.CellName = item.CellName;
            this.Description = item.Description;
            this.Lists = new HomeListModel[]
            {
                new HomeListModel("Elicitação"),
                new HomeListModel("Documentação"),
                new HomeListModel("Revisão"),
            };

            using (var ctx = new Entities())
            {
                List<HomeListItemModel> items = null;

                items = new List<HomeListItemModel>();
                var list = (from i in ctx.Elicitations where i.CellID == item.CellID orderby i.RequestDate descending select i).Take(HomeModel.LIST_ITEMS_QUANTITY).ToList();
                list.ForEach(i => items.Add(new HomeListItemModel(i.Summary, i.Text)));
                this.Lists[0].Items = items.ToArray();
            }
        }
    }

    public class HomeListModel
    {

        public string Title { get; set; }

        public HomeListItemModel[] Items { get; set; }

        public HomeListModel(string title)
        {
            this.Title = title;
            this.Items = new HomeListItemModel[0];
        }

    }

    public class HomeListItemModel
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public HomeListItemModel(string title, string description)
        {
            this.Title = title;
            this.Description = description.Replace("<p>", "").Replace("</p>", " ");
        }

    }
}
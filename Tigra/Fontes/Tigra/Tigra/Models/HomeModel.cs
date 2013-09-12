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

        public string Tag { get; set; }

        public string Description { get; set; }

        public HomeListModel[] Lists { get; set; }

        public HomeModel()
        {
            //
        }

        public HomeModel(Cell item)
        {
            this.CellName = item.CellName;
            this.Tag = item.Tag;
            this.Description = item.Description;
            this.Lists = new HomeListModel[]
            {
                new HomeListModel("Histórias", "Stories"),
                new HomeListModel("Requisitos", "Requirements"),
                new HomeListModel("Revisão", "Revision"),
            };

            using (var ctx = new Entities())
            {
                List<HomeListItemModel> items = null;

                items = new List<HomeListItemModel>();
                var list = ctx.GetLatestRequirements(item.CellID, null, 1).ToList();
                list.ForEach(i => items.Add(new HomeListItemModel(i.RequirementID, i.Title, i.Text)));
                this.Lists[0].Items = items.ToArray();
            }
        }
    }

    public class HomeListModel
    {

        public string Title { get; set; }

        public string Controller { get; set; }

        public HomeListItemModel[] Items { get; set; }

        public HomeListModel(string title, string controller)
        {
            this.Title = title;
            this.Controller = controller;
            this.Items = new HomeListItemModel[0];
        }

    }

    public class HomeListItemModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public HomeListItemModel(int id, string title, string description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description.Replace("<p>", "").Replace("</p>", " ");
        }

    }
}
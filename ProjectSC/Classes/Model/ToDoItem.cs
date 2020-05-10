using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


using ProjectSC.Classes;

namespace ProjectSC
{
    public class ToDoItem : IDataManipulation<ToDoItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }

        public bool CanNotify { get; set; }
        public int NotifyType { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public DateTime CreatedDateTime { get; set; }



        #region test data
        public void StoreTestData(List<ToDoItem> inventory)
        {
            inventory.Add(new ToDoItem { Id = 100, Title = "Notify Test", Description = "It works !" });
            inventory.Add(new ToDoItem { Id = 1, Title = "Update the software", Description = "windows sucks" });
            inventory.Add(new ToDoItem { Id = 2, Title = "Meeting", Description = "meet the CEO" });
            inventory.Add(new ToDoItem { Id = 3, Title = "Math Homework", Description = "kinda hard" });
            inventory.Add(new ToDoItem { Id = 4, Title = "Read Pro Angular 6", Description = "" });
            inventory.Add(new ToDoItem { Id = 5, Title = "Call Mom", Description = "No Signal" });
            inventory.Add(new ToDoItem { Id = 6, Title = "Review EE", Description = "study for the test" });
            inventory.Add(new ToDoItem { Id = 7, Title = "Programming", Description = "Angular + JS" });
            inventory.Add(new ToDoItem { Id = 8, Title = "EE test 1-1", Description = "RC coupler" });
            inventory.Add(new ToDoItem { Id = 9, Title = "Go home", Description = "just go" });
            inventory.Add(new ToDoItem { Id = 10, Title = "EE test 1-2~1-3", Description = "Direct coupler" });
            inventory.Add(new ToDoItem
            {
                Id = 11,
                Title = "Detail test",
                Description = "Ten years ago, I was nearly 30 and over $90,000 in debt. I had spent my twenties trying to build an interesting life; I had two degrees; I had lived in New York and the Bay Area; I had worked in a series of interesting jobs;I spent a lot of time traveling overseas. But I had also made a couple of critically stupid and shortsighted decisions. I had invested tens of thousands of dollars in a master's degree in landscape architecture that I realized I didn't want halfway through. While maxing out my student loans, I had also collected a toxic mix of maxed-out credit cards, personal loans, and $2,000 I had borrowed from my father for a crisis long since forgotten. My life consisted of loan deferments and minimum payments.Like so many other lost children,I had fallen into a career in IT.The work was boring,but led to jobs with cool organizations — a lot of jobs,because I kept quitting them.As soon as I had any money in the bank,I'd quit and go backpacking in Southeast Asia. My adventures were life-changing experiences, but I was eventually left with a CV that was pretty scattershot.My luck securing interesting jobs dried up.In 2001,I ended up living with my dad for four months and working at a banking infrastructure company in suburban Pittsburgh.I should have taken that as a warning that I needed to get it together, but I thought it was just an aberration.It was not.A year later, I was living in a one room Brooklyn apartment with a cat that compulsively peed on my bathmat. (This was a questionable upgrade to living with Dad.) I was trapped in a low paying IT job that I hated, I was in a relationship with a woman who hated me, and I was barely getting by, never mind getting control over my financial situation.I was finally reckoning with the fact that I was facing decades of unaffordable minimum debt payments. Any career ambitions I might have had would be put on hold indefinitely, possibly permanently. Travel was over.The full amount that I would have to pay off at the end of it all was too awful to contemplate.I felt trapped and hopeless.I was in a dark place.And so like many people in dark places, I looked for an escape hatch.I came up with two options: going back to graduate school or getting a job with a non - governmental organization(NGO) overseas.Graduate school would be the ultimate escape — it was better than working, and it would allow me to continue to defer my existing student loans(notwithstanding, of course, the small fact that I would potentially be doubling them). But while school was attractive in the short term, the financial implications were absolutely insane."
            });

            SaveToJson(inventory);
        }
        #endregion

        public void LoadFullData(ref List<ToDoItem> inventory)
        {
            string json = File.ReadAllText(@"inv.json");
            inventory = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
        }

        public void LoadTimeData(ref List<TimeRecord> timeRecords)
        {
            timeRecords.Clear();

            string json = File.ReadAllText(@"inv.json");
            List<ToDoItem> inventory = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].CanNotify)
                {
                    timeRecords.Add(new TimeRecord { Title = inventory[i].Title, BeginDateTime = inventory[i].BeginDateTime, EndDateTime = inventory[i].EndDateTime });
                }
            }
        }

        public void ResetId(List<ToDoItem> inventory)
        {
            foreach (var item in inventory)
            {
                item.Id = inventory.IndexOf(item);
            }

            SaveToJson(inventory);
        }



        public void AddNew(string title, string description, bool canNotify, DateTime begineDateTime, DateTime endDateTime, DateTime createdDateTime, List<ToDoItem> inventory)
        {
            int id = inventory.Count + 1;
            inventory.Add(new ToDoItem
            {
                Id = id,
                Title = title,
                Description = description,
                CanNotify = canNotify,
                BeginDateTime = begineDateTime,
                EndDateTime = endDateTime,
                CreatedDateTime = createdDateTime
            });
            SaveToJson(inventory);
        }

        public void Remove(int id, List<ToDoItem> inventory)
        {
            inventory.RemoveAt(id);
            SaveToJson(inventory);
        }

        public void SaveToJson(List<ToDoItem> inventory)
        {
            File.WriteAllText(@"inv.json", JsonConvert.SerializeObject(inventory));
        }

        public void Update(int id, string title, string description, bool canNotify, DateTime beginDateTime, DateTime endDateTime, List<ToDoItem> inventory)
        {
            inventory[id].Title = title;
            inventory[id].Description = description;
            inventory[id].CanNotify = canNotify;
            inventory[id].BeginDateTime = beginDateTime;
            inventory[id].EndDateTime = endDateTime;
        }



        public string FindById(int id, List<ToDoItem> inventory)
        {
            if (inventory.Exists(x => x.Id == id))
            {
                return inventory.Find(x => x.Id == id).ToString();
            }
            else
            {
                return $"\t{id} does not exist in the list";
            }
        }

        public string FindByTitle(string title, List<ToDoItem> inventory)
        {
            if (inventory.Exists(x => x.Title == title))
            {
                return inventory.Find(x => x.Title == title).ToString();
            }
            else
            {
                return $"\t{title} does not exist in the list";
            }
        }
    }
}

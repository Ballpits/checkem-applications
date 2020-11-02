using Newtonsoft.Json;
using ProjectSC.Models.Interface;
using ProjectSC.Models.Object.Tag;
using ProjectSC.Models.ToDo;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectSC.Models.DataAccess
{
    public class DataAccess_Json : IDataAccess
    {
        private string path = "Inventory.json";

        private static void SaveToJson(List<ToDoItem> inventory)
        {
            File.WriteAllText(@"Inventory.json", JsonConvert.SerializeObject(inventory));
        }



        #region test data
        public void StoreTestData(List<ToDoItem> inventory)
        {
            //inventory.Add(new ToDoItem { Id = 0, Title = "Item 0", Description = "Item 0's details !" });
            //inventory.Add(new ToDoItem { Id = 1, Title = "Item 1", Description = "Item 1's details !" });
            //inventory.Add(new ToDoItem { Id = 2, Title = "Item 2", Description = "Item 2's details !" });

            //inventory.Add(new ToDoItem { Id = 0, Title = "Notify Test", Description = "It works !" });

            inventory.Add(new ToDoItem { Id = 1, Title = "Update", Description = "windows 10 update" });
            inventory.Add(new ToDoItem { Id = 2, Title = "Meeting", IsStarred = true });
            inventory.Add(new ToDoItem { Id = 3, Title = "Math Homework", Description = "kinda hard" });
            inventory.Add(new ToDoItem { Id = 4, Title = "Read Pro Angular 6", IsCompleted = true });
            inventory.Add(new ToDoItem { Id = 5, Title = "Call someone", Description = "I forgot who it was ;p", IsStarred = true });
            inventory.Add(new ToDoItem { Id = 6, Title = "Review electronic", Description = "prepare for the test", IsStarred = true });
            inventory.Add(new ToDoItem { Id = 7, Title = "Program", Description = "Angular + JS", IsCompleted = true, IsStarred = true });
            inventory.Add(new ToDoItem { Id = 8, Title = "electronic test", Description = "1-1:RC coupler", IsStarred = true });
            inventory.Add(new ToDoItem { Id = 9, Title = "Math test", Description = "test ?! again ?!", IsCompleted = true });

            //inventory.Add(new ToDoItem
            //{
            //    Id = 10,
            //    Title = "Detail test",
            //    Description = "Ten years ago, I was nearly 30 and over $90,000 in debt. I had spent my twenties trying to build an interesting life; I had two degrees; I had lived in New York and the Bay Area; I had worked in a series of interesting jobs;I spent a lot of time traveling overseas. But I had also made a couple of critically stupid and shortsighted decisions. I had invested tens of thousands of dollars in a master's degree in landscape architecture that I realized I didn't want halfway through. While maxing out my student loans, I had also collected a toxic mix of maxed-out credit cards, personal loans, and $2,000 I had borrowed from my father for a crisis long since forgotten. My life consisted of loan deferments and minimum payments.Like so many other lost children,I had fallen into a career in IT.The work was boring,but led to jobs with cool organizations — a lot of jobs,because I kept quitting them.As soon as I had any money in the bank,I'd quit and go backpacking in Southeast Asia. My adventures were life-changing experiences, but I was eventually left with a CV that was pretty scattershot.My luck securing interesting jobs dried up.In 2001,I ended up living with my dad for four months and working at a banking infrastructure company in suburban Pittsburgh.I should have taken that as a warning that I needed to get it together, but I thought it was just an aberration.It was not.A year later, I was living in a one room Brooklyn apartment with a cat that compulsively peed on my bathmat. (This was a questionable upgrade to living with Dad.) I was trapped in a low paying IT job that I hated, I was in a relationship with a woman who hated me, and I was barely getting by, never mind getting control over my financial situation.I was finally reckoning with the fact that I was facing decades of unaffordable minimum debt payments. Any career ambitions I might have had would be put on hold indefinitely, possibly permanently. Travel was over.The full amount that I would have to pay off at the end of it all was too awful to contemplate.I felt trapped and hopeless.I was in a dark place.And so like many people in dark places, I looked for an escape hatch.I came up with two options: going back to graduate school or getting a job with a non - governmental organization(NGO) overseas.Graduate school would be the ultimate escape — it was better than working, and it would allow me to continue to defer my existing student loans(notwithstanding, of course, the small fact that I would potentially be doubling them). But while school was attractive in the short term, the financial implications were absolutely insane."
            //});

            SaveToJson(inventory);
        }
        #endregion



        public void RetrieveData(ref List<ToDoItem> inventory)
        {
            string json = File.ReadAllText(path);

            inventory = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
        }//Get all the data from the json file and save it to the list



        public void RetrieveTimeData(ref List<Object.Notification.Notifications> timeRecords)
        {
            string json = File.ReadAllText(path);

            List<ToDoItem> inventory = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].IsReminderOn)
                {
                    timeRecords.Add(new Object.Notification.Notifications { Title = inventory[i].Title, BeginDateTime = (DateTime)inventory[i].BeginDateTime, EndDateTime = (DateTime)inventory[i].EndDateTime });
                }
            }
        }//Get the time data and save it to the list



        public void ResetId(List<ToDoItem> inventory)
        {
            foreach (var item in inventory)
            {
                item.Id = inventory.IndexOf(item);
            }

            SaveToJson(inventory);
        }//Reset the id to align with stackpanel index

        public void Remove(ToDoItem toDoItem, List<ToDoItem> inventory)
        {
            inventory.Remove(toDoItem);
            SaveToJson(inventory);
        }//Remove the to-do item from the json file


        #region AddNew
        public void AddNew(ToDoItem toDoItem, List<ToDoItem> inventory)
        {
            inventory.Add(toDoItem);

            SaveToJson(inventory);
        }//Add new item
        #endregion



        #region Update
        public void Update(ToDoItem toDoItem, List<ToDoItem> inventory)
        {
            int index = inventory.FindIndex(x => x.Id == toDoItem.Id);

            inventory[index] = toDoItem;

            SaveToJson(inventory);
        }//Update
        #endregion
    }
}

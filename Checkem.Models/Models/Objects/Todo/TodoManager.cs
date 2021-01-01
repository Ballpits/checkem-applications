using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Checkem.Models
{
    public class TodoManager : Todo_DataAccess_Json<Todo>
    {
        public TodoManager()
        {
            JsonFilePath = @"./Inventory.json";
        }


        //Filter do to items
        #region Filter
        public List<Todo> Filter(FilterMethods method)
        {
            Inventory = Retrieve();

            switch (method)
            {
                //return all items in Inventory list
                case FilterMethods.None:
                    {
                        return Inventory;
                    }

                //return planned item in Inventory list
                case FilterMethods.Planned:
                    {
                        List<Todo> list = new List<Todo>();

                        foreach (var item in Inventory)
                        {
                            if (item.ReminderState != ReminderState.None)
                            {
                                list.Add(item);
                            }
                        }

                        return list;
                    }

                //return starred item in Inventory list
                case FilterMethods.Starred:
                    {
                        List<Todo> list = new List<Todo>();

                        foreach (var item in Inventory)
                        {
                            if (item.IsStarred)
                            {
                                list.Add(item);
                            }
                        }

                        return list;
                    }

                //return completed item in Inventory list
                case FilterMethods.Completed:
                    {
                        List<Todo> list = new List<Todo>();

                        foreach (var item in Inventory)
                        {
                            if (item.IsCompleted)
                            {
                                list.Add(item);
                            }
                        }

                        return list;
                    }

                //return completed item in Inventory list
                case FilterMethods.TaskToday:
                    {
                        List<Todo> list = new List<Todo>();

                        foreach (var item in Filter(FilterMethods.Planned))
                        {
                            switch (item.ReminderState)
                            {
                                case ReminderState.Basic:
                                    {
                                        if (item.EndDateTime.Value.Date == DateTime.Today)
                                        {
                                            list.Add(item);
                                        }

                                        break;
                                    }
                                case ReminderState.Advance:
                                    {
                                        if (item.BeginDateTime.Value.Date == DateTime.Today || item.EndDateTime.Value.Date == DateTime.Today)
                                        {
                                            list.Add(item);
                                        }

                                        break;
                                    }
                                default:
                                    break;
                            }
                        }

                        return list;
                    }

                default:
                    {
                        return null;
                    }

            }
        }
        #endregion


        //Sort and return Inventory list
        #region Sort
        public List<Todo> Sort(SortMethods method, List<Todo> list)
        {
            switch (method)
            {
                //return Inventory ordered by ID
                case SortMethods.ID:
                    {
                        return list.OrderBy(x => x.ID).ToList();
                    }

                //return Inventory ordered by ID reversed
                case SortMethods.IDReversed:
                    {
                        return list.OrderBy(x => x.ID).Reverse().ToList();
                    }

                //return Inventory ordered by Importance(starred first)
                case SortMethods.StarredFirst:
                    {
                        return list.OrderBy(x => x.IsStarred).Reverse().ToList();
                    }

                //return Inventory ordered by alphabetical ascending
                case SortMethods.AlphabeticalAscending:
                    {
                        return list.OrderBy(x => x.Title).ToList();
                    }

                //return Inventory ordered by alphabetical descending
                case SortMethods.AlphabeticalDescending:
                    {
                        return list.OrderBy(x => x.Title).Reverse().ToList();
                    }

                //return Inventory ordered by begin time
                case SortMethods.BeginTime:
                    {
                        return list.OrderBy(x => x.BeginDateTime).ToList();
                    }

                //return Inventory ordered by end time
                case SortMethods.EndTime:
                    {
                        return list.OrderBy(x => x.EndDateTime).ToList();
                    }

                //return Inventory ordered by creation date
                case SortMethods.CreationDate:
                    {
                        return list.OrderBy(x => x.CreationDateTime).ToList();
                    }

                default:
                    {
                        return null;
                    }
            }
        }
        #endregion


        //Rearrange id according to the index in Inventory list
        #region ResetId
        public void ResetId()
        {
            foreach (var item in Inventory)
            {
                item.ID = Inventory.IndexOf(item);
            }

            Save(Inventory);
        }
        #endregion


        //Find to do items
        #region FindAll
        public List<Todo> FindAll(string searchString)
        {
            //Ignore casing
            return Inventory.FindAll(x => x.Title.ToLower().Contains(searchString.ToLower()));
        }
        #endregion


        //Return test to do data
        #region StoreTestData
        public void StoreTestData()
        {
            Inventory.Clear();

            //Inventory.Add(new Todo { ID = 0, Title = "Item 0", Description = "Item 0's details !" });
            //Inventory.Add(new Todo { ID = 1, Title = "Item 1", Description = "Item 1's details !" });
            //Inventory.Add(new Todo { ID = 2, Title = "Item 2", Description = "Item 2's details !" });

            Inventory.Add(new Todo { ID = 0, Title = "Notify Test 0", Description = "It works !", ReminderState = ReminderState.Basic, EndDateTime = DateTime.Now });
            //Inventory.Add(new Todo { ID = 1, Title = "Notify Test 1", Description = "It works !", ReminderState = ReminderState.Basic, EndDateTime = DateTime.Now });

            //List<TagItem> tagItems = new List<TagItem>();
            //tagItems.Add(new TagItem() { ID = 0, Content = "test tag", TagColor = Brushes.Brown });
            //tagItems.Add(new TagItem() { ID = 1, Content = "test tag", TagColor = Brushes.PaleVioletRed });
            //tagItems.Add(new TagItem() { ID = 2, Content = "test tag", TagColor = Brushes.GreenYellow });
            //tagItems.Add(new TagItem() { ID = 3, Content = "test tag", TagColor = Brushes.BlueViolet });
            //tagItems.Add(new TagItem() { ID = 4, Content = "test tag", TagColor = Brushes.Orange });

            //Inventory.Add(new Todo { ID = 0, Title = "Tag Test", Description = "It works !", /*TagItems = tagItems*/ });

            //Inventory.Add(new Todo { ID = 1, Title = "Update", Description = "windows 10 update", IsReminderOn = true, EndDateTime = DateTime.Now.AddDays(5) });
            //Inventory.Add(new Todo { ID = 2, Title = "Meeting", IsStarred = true });
            //Inventory.Add(new Todo { ID = 3, Title = "Math Homework", Description = "kinda hard", IsReminderOn = true, EndDateTime = DateTime.Now.AddMinutes(30) });
            //Inventory.Add(new Todo { ID = 4, Title = "Read Pro Angular 6", IsCompleted = true, IsReminderOn = true, IsAdvanceReminderOn = true, BeginDateTime = DateTime.Now.AddMinutes(1), EndDateTime = DateTime.Now.AddHours(1) });
            //Inventory.Add(new Todo { ID = 5, Title = "Call someone", Description = "I forgot who it was ;p", IsStarred = true });
            //Inventory.Add(new Todo { ID = 6, Title = "Review electronic", Description = "prepare for the test", IsStarred = true });
            //Inventory.Add(new Todo { ID = 7, Title = "Program", Description = "Angular + JS", IsCompleted = true, IsStarred = true });
            //Inventory.Add(new Todo { ID = 8, Title = "electronic test", Description = "1-1:RC coupler", IsStarred = true });
            //Inventory.Add(new Todo { ID = 9, Title = "Math test", Description = "test ?! again ?!", IsCompleted = true, IsReminderOn = true, EndDateTime = DateTime.Now.AddDays(1) });

            //Inventory.Add(new Todo
            //{
            //    ID = 10,
            //    Title = "Detail test",
            //    Description = "Ten years ago, I was nearly 30 and over $90,000 in debt. I had spent my twenties trying to build an interesting life; I had two degrees; I had lived in New York and the Bay Area; I had worked in a series of interesting jobs;I spent a lot of time traveling overseas. But I had also made a couple of critically stupid and shortsighted decisions. I had invested tens of thousands of dollars in a master's degree in landscape architecture that I realized I didn't want halfway through. While maxing out my student loans, I had also collected a toxic mix of maxed-out credit cards, personal loans, and $2,000 I had borrowed from my father for a crisis long since forgotten. My life consisted of loan deferments and minimum payments.Like so many other lost children,I had fallen into a career in IT.The work was boring,but led to jobs with cool organizations — a lot of jobs,because I kept quitting them.As soon as I had any money in the bank,I'd quit and go backpacking in Southeast Asia. My adventures were life-changing experiences, but I was eventually left with a CV that was pretty scattershot.My luck securing interesting jobs dried up.In 2001,I ended up living with my dad for four months and working at a banking infrastructure company in suburban Pittsburgh.I should have taken that as a warning that I needed to get it together, but I thought it was just an aberration.It was not.A year later, I was living in a one room Brooklyn apartment with a cat that compulsively peed on my bathmat. (This was a questionable upgrade to living with Dad.) I was trapped in a low paying IT job that I hated, I was in a relationship with a woman who hated me, and I was barely getting by, never mind getting control over my financial situation.I was finally reckoning with the fact that I was facing decades of unaffordable minimum debt payments. Any career ambitions I might have had would be put on hold indefinitely, possibly permanently. Travel was over.The full amount that I would have to pay off at the end of it all was too awful to contemplate.I felt trapped and hopeless.I was in a dark place.And so like many people in dark places, I looked for an escape hatch.I came up with two options: going back to graduate school or getting a job with a non - governmental organization(NGO) overseas.Graduate school would be the ultimate escape — it was better than working, and it would allow me to continue to defer my existing student loans(notwithstanding, of course, the small fact that I would potentially be doubling them). But while school was attractive in the short term, the financial implications were absolutely insane."
            //});

            Save(Inventory);
        }
        #endregion


        //Add new item and save to database
        public void Add(Todo data)
        {
            Inventory.Add(data);

            Save(Inventory);
        }


        //Update item and save to database
        public void Update(Todo data)
        {
            //Find to do item's index in Inventory than update
            Inventory[Inventory.IndexOf(data)] = data;

            Save(Inventory);
        }


        //Remove item and save to database
        public void Remove(Todo data)
        {
            Inventory.Remove(data);

            Save(Inventory);
        }
    }
}

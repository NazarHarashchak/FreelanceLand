using FreelanceLand.Models;
using System;
using System.Linq;
using TASK = FreelanceLand.Models.Task;
using TASK_STATUS = FreelanceLand.Models.TaskStatus;

namespace Backend
{
    public class SampleData
    {
        public static void Initialize(ApplicationContext context)
        {
            UserRoles UserRole1 = new UserRoles
            {
                Type = "User"
            };
            UserRoles UserRole2 = new UserRoles
            {
                Type = "Administrator"
            };
            UserRoles UserRole3 = new UserRoles
            {
                Type = "Moderator"
            };
            
            User user1 = new User
            {
                Name = "Anton",
                Sur_Name = "Ivanov",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "123@gmail.com",
                Login = "4UprRp",
                Password = BCrypt.Net.BCrypt.HashPassword("6cAM7F"),
                UserRole = UserRole2
            };
            User user2 = new User
            {
                Name = "Ivan",
                Sur_Name = "Antonov",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "1234@gmail.com",
                Login = "Py2fGc",
                Password = BCrypt.Net.BCrypt.HashPassword("NweL7R"),
                UserRole = UserRole3
            };
            User user3 = new User
            {
                Name = "Aleks",
                Sur_Name = "Ivanov",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "12345@gmail.com",
                Login = "4bsCNG",
                Password = BCrypt.Net.BCrypt.HashPassword("5mGU9u"),
                UserRole = UserRole3
            };
            User user4 = new User
            {
                Name = "Peter",
                Sur_Name = "Pete",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "123456@gmail.com",
                Login = "W8P3vN",
                Password = BCrypt.Net.BCrypt.HashPassword("G5mhTF"),
                UserRole = UserRole1
            };
            User user5 = new User
            {
                Name = "John",
                Sur_Name = "Doe",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "1234567@gmail.com",
                Login = "3H3jaX",
                Password = BCrypt.Net.BCrypt.HashPassword("Z8GtN2"),
                UserRole = UserRole1
            };
            User user6 = new User
            {
                Name = "Jane",
                Sur_Name = "Doe",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "12345678@gmail.com",
                Login = "qU5gFf",
                Password = BCrypt.Net.BCrypt.HashPassword("Le7jCr"),
                UserRole = UserRole1
            };
            User user7 = new User
            {
                Name = "Efie",
                Sur_Name = "Jazz",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "123456789@gmail.com",
                Login = "xv5TzK",
                Password = BCrypt.Net.BCrypt.HashPassword("Na3mgu"),
                UserRole = UserRole1
            };
            User user8 = new User
            {
                Name = "Soft",
                Sur_Name = "Serve",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "1234567890@gmail.com",
                Login = "BS9GaS",
                Password = BCrypt.Net.BCrypt.HashPassword("U8V6uX"),
                UserRole = UserRole1
            };
            User user9 = new User
            {
                Name = "Avrelii",
                Sur_Name = "Vrozlav",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "12345678901@gmail.com",
                Login = "qRg22k",
                Password = BCrypt.Net.BCrypt.HashPassword("uyHK3g"),
                UserRole = UserRole1
            };
            User user10 = new User
            {
                Name = "Corn",
                Sur_Name = "Pub",
                Birth_Date = new DateTime(1999, 1, 1),
                Phone_Number = "+38(***)**-**-***",
                Email = "123456789012@gmail.com",
                Login = "v4xQ2c",
                Password = BCrypt.Net.BCrypt.HashPassword("Ez96k6"),
                UserRole = UserRole1
            };

            TaskCategory taskCategory1 = new TaskCategory
            {
                Type = "Web development"
            };
            TaskCategory taskCategory2 = new TaskCategory
            {
                Type = "Desktop development"
            };
            TaskCategory taskCategory3 = new TaskCategory
            {
                Type = "Android development"
            };
            TaskCategory taskCategory4 = new TaskCategory
            {
                Type = "Game development"
            };

            TASK_STATUS taskStatus1 = new TASK_STATUS
            {
                Type = "In progress"
            };
            TASK_STATUS taskStatus2 = new TASK_STATUS
            {
                Type = "Done"
            };
            TASK_STATUS taskStatus3 = new TASK_STATUS
            {
                Type = "To do"
            };
            TASK_STATUS taskStatus4 = new TASK_STATUS
            {
                Type = "Ready for veryfycation"
            };

            TASK task1 = new TASK
            {
                Price = 1,
                Title = "Task1",
                Description = "Some task description1",
                DateCreate = new DateTime(2019, 1, 1),
                DateUpdated = new DateTime(2019, 1, 2),
                TaskStatus = taskStatus2,
                TaskCategory = taskCategory1
            };
            TASK task2 = new TASK
            {
                Price = 2,
                Title = "Task2",
                Description = "Some task description2",
                DateCreate = new DateTime(2019, 2, 1),
                DateUpdated = new DateTime(2019, 2, 2),
                TaskStatus = taskStatus1,
                TaskCategory = taskCategory2
            };
            TASK task3 = new TASK
            {
                Price = 3,
                Title = "Task3",
                Description = "Some task description3",
                DateCreate = new DateTime(2019, 3, 1),
                DateUpdated = new DateTime(2019, 3, 2),
                TaskStatus = taskStatus3,
                TaskCategory = taskCategory1
            };
            TASK task4 = new TASK
            {
                Price = 4,
                Title = "Task4",
                Description = "Some task description4",
                DateCreate = new DateTime(2019, 4, 1),
                DateUpdated = new DateTime(2019, 4, 2),
                TaskStatus = taskStatus1,
                TaskCategory = taskCategory3
            };
            TASK task5 = new TASK
            {
                Price = 5,
                Title = "Task5",
                Description = "Some task description5",
                DateCreate = new DateTime(2019, 5, 1),
                DateUpdated = new DateTime(2019, 5, 2),
                TaskStatus = taskStatus1,
                TaskCategory = taskCategory4
            };
            TASK task6 = new TASK
            {
                Price = 6,
                Title = "Task6",
                Description = "Some task description6",
                DateCreate = new DateTime(2019, 6, 1),
                DateUpdated = new DateTime(2019, 6, 2),
                TaskStatus = taskStatus2,
                TaskCategory = taskCategory4
            };
            TASK task7 = new TASK
            {
                Price = 7,
                Title = "Task7",
                Description = "Some task description7",
                DateCreate = new DateTime(2019, 7, 1),
                DateUpdated = new DateTime(2019, 7, 2),
                TaskStatus = taskStatus3,
                TaskCategory = taskCategory2
            };
            TASK task8 = new TASK
            {
                Price = 8,
                Title = "Task8",
                Description = "Some task description8",
                DateCreate = new DateTime(2019, 8, 1),
                DateUpdated = new DateTime(2019, 8, 2),
                TaskStatus = taskStatus3,
                TaskCategory = taskCategory3
            };
            TASK task9 = new TASK
            {
                Price = 9,
                Title = "Task9",
                Description = "Some task description9",
                DateCreate = new DateTime(2019, 9, 1),
                DateUpdated = new DateTime(2019, 9, 2),
                TaskStatus = taskStatus2,
                TaskCategory = taskCategory1
            };
            TASK task10 = new TASK
            {
                Price = 10,
                Title = "Task10",
                Description = "Some task description10",
                DateCreate = new DateTime(2019, 10, 1),
                DateUpdated = new DateTime(2019, 10, 2),
                TaskStatus = taskStatus3,
                TaskCategory = taskCategory2
            };

            Review review1 = new Review
            {
                Date = new DateTime(2018, 10, 1),
                Description = "Good job!",
                ExecutorUser = user6,
                CustomerUser = user1
            };
            Review review2 = new Review
            {
                Date = new DateTime(2018, 10, 2),
                Description = "Not bad!",
                ExecutorUser = user7,
                CustomerUser = user2
            };
            Review review3 = new Review
            {
                Date = new DateTime(2018, 10, 3),
                Description = "Great!",
                ExecutorUser = user8,
                CustomerUser = user3
            };
            Review review4 = new Review
            {
                Date = new DateTime(2018, 10, 4),
                Description = "Nice worker!",
                ExecutorUser = user9,
                CustomerUser = user4
            };
            Review review5 = new Review
            {
                Date = new DateTime(2018, 10, 5),
                Description = "I like this man!",
                ExecutorUser = user10,
                CustomerUser = user5
            };

            Message message1 = new Message
            {
                Content = "Hi",
                DateAndTime = new DateTime(2018, 9, 4, 18, 30, 25),
                SenderUser = user1,
            };
            Message message2 = new Message
            {
                Content = "Hello",
                DateAndTime = new DateTime(2018, 9, 4, 18, 31, 25),
                SenderUser = user6,
            };
            Message message3 = new Message
            {
                Content = "What about my task?",
                DateAndTime = new DateTime(2018, 9, 4, 20, 30, 25),
                SenderUser = user3,
            };
            Message message4 = new Message
            {
                Content = "It is in progress",
                DateAndTime = new DateTime(2018, 9, 4, 22, 30, 25),
                SenderUser = user4,
            };
            Message message5 = new Message
            {
                Content = "Nice job, thank you.",
                DateAndTime = new DateTime(2018, 9, 4, 18, 30, 25),
                SenderUser = user3,
            };

            Comment comment1 = new Comment
            {
                Content = "I want do it!",
                DateAndTime = new DateTime(2018, 9, 4, 22, 30, 25),
                User = user6,
                Task = task2
            };
            Comment comment2 = new Comment
            {
                Content = "I can do it!",
                DateAndTime = new DateTime(2018, 10, 4, 22, 30, 25),
                User = user8,
                Task = task3
            };
            Comment comment3 = new Comment
            {
                Content = "I want do it!",
                DateAndTime = new DateTime(2018, 9, 4, 22, 30, 25),
                User = user7,
                Task = task9
            };
            Comment comment4 = new Comment
            {
                Content = "What about deadlines!",
                DateAndTime = new DateTime(2018, 10, 4, 22, 30, 25),
                User = user10,
                Task = task1
            };
            Comment comment5 = new Comment
            {
                Content = "I can",
                DateAndTime = new DateTime(2018, 9, 4, 23, 30, 25),
                User = user5,
                Task = task6
            };

            Notification noti = new Notification
            {
                Message = "Your comment was deleted by moderator",
                DateAndTime = DateTime.Now,
                User = user2
            };

            TaskHistory taskHistory1 = new TaskHistory
            {
                DateUpdated = new DateTime(2018, 9, 4),
                TaskCustomer = user1,
                Task = task1
            };
            TaskHistory taskHistory2 = new TaskHistory
            {
                DateUpdated = new DateTime(2018, 10, 4),
                TaskCustomer = user2,
                Task = task2
            };
            TaskHistory taskHistory3 = new TaskHistory
            {
                DateUpdated = new DateTime(2018, 11, 4),
                TaskCustomer = user3,
                Task = task3
            };
            TaskHistory taskHistory4 = new TaskHistory
            {
                DateUpdated = new DateTime(2018, 9, 4),
                TaskCustomer = user4,
                Task = task4
            };
            TaskHistory taskHistory5 = new TaskHistory
            {
                DateUpdated = new DateTime(2018, 12, 4),
                TaskCustomer = user5,
                Task = task1
            };

            
            if (!context.UserRoles.Any())
            {
                context.UserRoles.AddRange(
                        UserRole1,
                        UserRole2,
                        UserRole3
                    );
                context.SaveChanges();
            }
            if (!context.TaskCategories.Any())
            {
                context.TaskCategories.AddRange(
                        taskCategory1,
                        taskCategory2,
                        taskCategory3,
                        taskCategory4
                    );
                context.SaveChanges();
            }
            if (!context.TaskStatuses.Any())
            {
                context.TaskStatuses.AddRange(
                        taskStatus1,
                        taskStatus2,
                        taskStatus3
                    );
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                        user1,
                        user2,
                        user3,
                        user4,
                        user5,
                        user6,
                        user7,
                        user8,
                        user9,
                        user10
                    );
                context.SaveChanges();
            }
            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(
                        task1,
                        task2,
                        task3,
                        task4,
                        task5,
                        task6,
                        task7,
                        task8,
                        task9,
                        task10
                    );
                context.SaveChanges();
            }
            if (!context.Messages.Any())
            {
                context.Messages.AddRange(
                        message1,
                        message2,
                        message3,
                        message4,
                        message5
                    );
                context.SaveChanges();
            }
            if (!context.Reviews.Any())
            {
                context.Reviews.AddRange(
                        review1,
                        review2,
                        review3,
                        review4,
                        review5
                    );
                context.SaveChanges();
            }
            if (!context.Comments.Any())
            {
                context.Comments.AddRange(
                        comment1,
                        comment2,
                        comment3,
                        comment4,
                        comment5
                    );
                context.SaveChanges();
            }
            if (!context.Notifications.Any())
            {
                context.Notifications.AddRange(
                    noti
                    );
                context.SaveChanges();
            }
            if (!context.TaskHistories.Any())
            {
                context.TaskHistories.AddRange(
                        taskHistory1,
                        taskHistory2,
                        taskHistory3,
                        taskHistory4,
                        taskHistory5
                    );
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Xml;

namespace PrivateMultipleObjects
{
    // base class
    class Club
    {
        protected int _id;
        protected string _FirstName;
        protected string _LastName;

        // default contructor
        public Club()
        {
            _id = 0;
            _FirstName = string.Empty;
            _LastName = string.Empty;
        }

        // parameterized constructor
        public Club(int id, string firstName, string lastName)
        {
            _id = id;
            _FirstName = firstName;
            _LastName = lastName;
        }
        public virtual void addChange()
        {
            Console.Write("ID =");
            _id = int.Parse(Console.ReadLine());
            Console.Write("Member First Name: ");
            _FirstName = Console.ReadLine();
            Console.Write("Member Last Name: ");
            _LastName = Console.ReadLine();
        }
        public virtual void print()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"             ID: {_id}");
            Console.WriteLine($"           Name: {_FirstName} {_LastName}");
            Console.WriteLine();
        }
    }

    // derived class
    class BookClub : Club
    {
        protected string _bookTitle;
        protected string _author;
        protected int _amtPages;

        // default contructor
        public BookClub()
        {
            _bookTitle = string.Empty;
            _author = string.Empty;
            _amtPages = 0;
        }

        // parameterized constructor
        public BookClub(int id, string firstname, string lastname, string booktitle, string author, int amtpages)
        {
            _bookTitle = booktitle;
            _author = author;
            _amtPages = amtpages;
        }

        public override void addChange()
        {

            Console.Write("Book Title: ");
            _bookTitle = Console.ReadLine();
            Console.Write("Author: ");
            _author = Console.ReadLine();
            Console.Write("Amount of pages: ");
            _amtPages = int.Parse(Console.ReadLine());
        }
        public override void print()
        {
            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"     Book Title: {_bookTitle}");
            Console.WriteLine($"         Author: {_author}");
            Console.WriteLine($"Amount of Pages: {_amtPages}");
            Console.WriteLine();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many club members do you want to enter?");
            int maxClub;
            while (!int.TryParse(Console.ReadLine(), out maxClub))
                Console.WriteLine("Please enter a whole number!");
            Club[] club = new Club[maxClub];
            Console.WriteLine("How many new books do you want to enter?");
            int maxBooks;
            while (!int.TryParse(Console.ReadLine(), out maxBooks))
                Console.WriteLine("Please enter a while number!");
            BookClub[] books = new BookClub[maxBooks];

            int choice, record, type;
            int clubCounter = 0, bookCounter = 0;
            choice = Menu();
            while (choice != 4)
            {
                Console.WriteLine("Enter 1 for Book List or 2 for Club Member List");
                while (!int.TryParse(Console.ReadLine(), out type))
                    Console.WriteLine("1 for Book List or 2 for Club Member List");
                try
                {
                    switch (choice)
                    {
                        case 1:
                            if (type == 1)
                            {
                                if (bookCounter <= maxBooks)
                                {
                                    books[bookCounter] = new BookClub();
                                    books[bookCounter].addChange();
                                    bookCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of books has been added.");
                            }
                            else
                            {
                                if (clubCounter <= maxClub)
                                {
                                    club[clubCounter] = new Club();
                                    club[clubCounter].addChange();
                                    clubCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of club members has been added.");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter the record number your want to change: ");
                            while (!int.TryParse(Console.ReadLine(), out record))
                                Console.WriteLine("Enter the record number you want to change: ");
                            record--;
                            if (type == 1)
                            {
                                while (record > bookCounter - 1 || record < 0)
                                {
                                    Console.WriteLine("The number you endered was out of range, try again!");
                                    while (!int.TryParse(Console.ReadLine(), out record))
                                        Console.Write("Enter the record number you want to change: ");
                                    record--;
                                }
                                books[record].addChange();
                            }
                            else
                            {
                                while (record > clubCounter - 1 || record > 0)
                                {
                                    Console.WriteLine("The number you endered was out of range, try again");
                                    while (!int.TryParse(Console.ReadLine(), out record))
                                        Console.Write("Enter the record number you want to change: ");
                                    record--;
                                }
                                club[record].addChange();
                            }
                            break;
                        case 3:
                            if (type == 1)
                            {
                                for (int i = 0; i < bookCounter; i++)
                                    books[i].print();
                            }
                            else
                            {
                                for (int i = 0; i < clubCounter; i++)
                                    club[i].print();
                            }
                            break;
                        default:
                            Console.WriteLine("You made an invalid selection, please try again!");
                            break;

                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                choice = Menu();
            }
        }
        private static int Menu()
        {
            Console.WriteLine("Please make a selection from the menu: ");
            Console.WriteLine("1-Add   2-Change    3-Print    4-Quit");
            int selection = 0;
            while (selection < 1 || selection > 4)
                while (!int.TryParse(Console.ReadLine(), out selection))
                    Console.WriteLine("1-Add   2-Change    3-Print    4-Quit");
            return selection;
        }
    }
}
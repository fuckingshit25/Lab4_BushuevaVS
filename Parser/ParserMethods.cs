using ClassLibrary_forDB;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class ParserMethods
    {
        public List<User> Parser()
        {
            

            string forumUrl = "https://www.woman.ru/health/medley7/thread/4540231/"; // URL страницы форума для парсинга

            // Создание объекта для работы с HTML
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(forumUrl);

            // Выбор всех сообщений на странице форума
            HtmlNodeCollection messageNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'card__container')]");

            List<User> users = new List<User>();
            // Извлечение информации из каждого сообщения
            foreach (HtmlNode messageNode in messageNodes)
            {
                // Извлечение имени пользователя
                HtmlNode userNode = messageNode.SelectSingleNode(".//div[contains(@class, 'user__name')]");
                string name = userNode?.InnerText.Trim() ?? string.Empty;
                //Console.WriteLine("Имя пользователя: " + name);

                // Извлечение ID сообщения
                HtmlNode idNode = messageNode.SelectSingleNode(".//div[contains(@class, 'card__message-data')]/div");
                int messageId = int.Parse(idNode?.InnerText.Trim('#') ?? "0");
                //Console.WriteLine("ID: " + messageId);

                // Извлечение текста сообщения
                HtmlNode textNode = messageNode.SelectSingleNode(".//p[contains(@class, 'card__comment')]");
                string text = textNode?.InnerText.Trim() ?? string.Empty;
                //Console.WriteLine("Текст сообщения: " + text);
                // Создание объекта User и добавление его в список
                User user = new User
                {
                    ID= messageId,
                    name = name,
                    message = text
                };
                users.Add(user);
            }
            return users;
        }
    }
}

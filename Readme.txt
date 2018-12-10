This single page application was part of assignments in subject Web Applications.
Folowing packages should be installed:
Microsoft.AspNetCore.App v2.1.1
Microsoft.AspNetCore.Razor.Design v2.1.2
Microsoft.AspNetCore.SpaServices.Extensions v2.1.1
Microsoft.NETCore.App v2.1.0
Microsoft.VisualStudio.Web.CodeGeneration.Design v2.1.1

You will need to change connection string in order to use your local server, and run following scripts in database:
1.  
INSERT INTO [dbo].[Categories] ([title]) VALUES (N'DIFFERENT QUESTIONS')
INSERT INTO [dbo].[Categories] ([title]) VALUES (N'GENERAL QUESTIONS')
INSERT INTO [dbo].[Categories] ([title]) VALUES (N'PAYMENT QUESTIONS')

2.
INSERT INTO [dbo].[Customers] ([name], [surname], [email]) VALUES (N'Iryna', N'Martynchuk', N's315562@oslomet.no')
INSERT INTO [dbo].[Customers] ([name], [surname], [email]) VALUES (N'Olena', N'Martynchuk', N'chycha33@yahoo.com')

3.
SET IDENTITY_INSERT [dbo].[Questions] ON
INSERT INTO [dbo].[Questions] ([id], [question], [answer], [thumbup], [thumbdown], [date], [categorytitle], [customername]) VALUES (1, N'Instead of video player I see white screen, what is wrong?', N'Try cleaning your browser cache, refreshing page, check link in another browser.', 0, 1, N'2018-11-12 10:12:30', N'GENERAL QUESTIONS', NULL)
INSERT INTO [dbo].[Questions] ([id], [question], [answer], [thumbup], [thumbdown], [date], [categorytitle], [customername]) VALUES (2, N'I payed for the movie, but did not get email with link?', N'Please send an email with information about transaction to our help center help@movies.com', 5, 1, N'2018-11-12 10:18:14', N'PAYMENT QUESTIONS', NULL)
INSERT INTO [dbo].[Questions] ([id], [question], [answer], [thumbup], [thumbdown], [date], [categorytitle], [customername]) VALUES (3, N'Is it any possibility to buy a year membership, if I do not want to pay separately for each movie?', N'We are currently working on this, this functionality will be available in term of the next two months.', 0, 1, N'2018-11-12 10:21:53', N'PAYMENT QUESTIONS', NULL)
INSERT INTO [dbo].[Questions] ([id], [question], [answer], [thumbup], [thumbdown], [date], [categorytitle], [customername]) VALUES (4, N'Which browser is it better to use to watch movie?', N'It is better to use Google Chrome or Mozilla browsers, in other browsers site works, but some functionality may not work.', 2, 0, N'2018-11-12 10:28:21', N'GENERAL QUESTIONS', NULL)
INSERT INTO [dbo].[Questions] ([id], [question], [answer], [thumbup], [thumbdown], [date], [categorytitle], [customername]) VALUES (5, N'You do not have the movie I want, is it possible to order a specific movie?', N'Yes, you can order a movie, just write an email to help@movie.com and specify "Subject" as Order movie.', 8, 2, N'2018-11-12 00:00:00', N'DIFFERENT QUESTIONS', N'Iryna')
INSERT INTO [dbo].[Questions] ([id], [question], [answer], [thumbup], [thumbdown], [date], [categorytitle], [customername]) VALUES (6, N'Is it possible to cancel my order?', N'You can contact our help center help@movie.com to cancel your order if it has not been processed yet. ', 6, 1, N'2018-11-16 00:00:00', N'DIFFERENT QUESTIONS', N'Olena')
SET IDENTITY_INSERT [dbo].[Questions] OFF

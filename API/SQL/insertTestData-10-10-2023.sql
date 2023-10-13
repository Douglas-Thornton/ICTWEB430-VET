-- Insert data into User table
INSERT INTO [dbo].[User] ([FirstName], [Surname], [PhoneNumber], [Email], [Suburb], [Postcode], [LoginUsername], [LoginPassword])
VALUES
('Alice', 'Johnson', '1112223333', 'alice.j@example.com', 'Cityville', '12345', 'alice_j', 'pass123'),
('Bob', 'Miller', '4445556666', 'bob.m@example.com', 'Townsville', '54321', 'bob_m', 'securepass'),
('Charlie', 'Davis', '7778889999', 'charlie.d@example.com', 'Villageville', '67890', 'charlie_d', 'pwd123'),
('David', 'Smith', '2223334444', 'david.s@example.com', 'Hamletville', '98765', 'david_s', 'password123');

-- Insert data into Pet table
INSERT INTO [dbo].[Pet] ([OwnerID], [PetName], [PetBreed], [PetAge], [PetGender], [PetDiscoverability], [PetPhoto], [PetPhotoFileLocation])
VALUES
(1, 'Max', 'Labrador Retriever', 2, 'Male', 1, NULL, NULL),
(2, 'Mittens', 'Siamese Cat', 3, 'Female', 1, NULL, NULL),
(3, 'Rocky', 'German Shepherd', 4, 'Male', 1, NULL, NULL),
(4, 'Whiskers', 'Persian Cat', 2, 'Male', 1, NULL, NULL),
(1, 'Luna', 'Golden Retriever', 1, 'Female', 1, NULL, NULL),
(3, 'Bella', 'Beagle', 3, 'Female', 1, NULL, NULL),
(4, 'Oliver', 'Bengal Cat', 2, 'Male', 1, NULL, NULL),
(2, 'Lucy', 'Dachshund', 5, 'Female', 1, NULL, NULL);

-- Insert data into Meeting table
INSERT INTO [dbo].[Meeting] ([UserCreated], [MeetingDate], [MeetingLocation], [MeetingCreationDate], [MeetingCancellationDate], [MeetingName], [MeetingMessage])
VALUES
(1, '2023-10-20 14:00:00', 'Park', '2023-10-10', NULL, 'Dog Owners Meetup', 'Socialize and playtime for our furry friends!'),
(2, '2023-10-25 18:30:00', 'Community Center', '2023-10-15', NULL, 'Cat Lovers Gathering', 'Discussing cat care tips and tricks.'),
(3, '2023-11-05 12:00:00', 'Beach', '2023-10-25', NULL, 'Pet Picnic', 'Bring your pets and enjoy a picnic by the sea!'),
(4, '2023-11-10 10:30:00', 'Dog Park', '2023-11-01', NULL, 'Small Dog Playdate', 'For small dog breeds to socialize and have fun.');

-- Insert data into InvitedUser table
INSERT INTO [dbo].[InvitedUser] ([UserID], [MeetingID], [Accepted], [ResponseDate])
VALUES
(1, 1, 1, '2023-10-15 09:30:00'),
(2, 1, 0, NULL),
(3, 1, 1, '2023-10-14 14:45:00'),
(4, 2, 1, '2023-10-18 16:20:00'),
(1, 3, 0, NULL),
(2, 3, 1, '2023-10-30 11:10:00'),
(3, 3, 1, '2023-11-01 08:45:00'),
(4, 4, 1, '2023-11-05 09:00:00');

-- Insert data into InvitedPet table
INSERT INTO [dbo].[InvitedPet] ([PetID], [InviteID])
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8);

-- Insert data into AppPreferences table
INSERT INTO [dbo].[AppPreferences] ([UserID], [WebpageAnimalPreference], [SelectedFont], [SelectedFontSize])
VALUES
(1, 'Dogs', 'Arial', '12pt'),
(2, 'Cats', 'Times New Roman', '14pt'),
(3, 'Both', 'Verdana', '16pt'),
(4, 'Dogs', 'Courier New', '10pt');

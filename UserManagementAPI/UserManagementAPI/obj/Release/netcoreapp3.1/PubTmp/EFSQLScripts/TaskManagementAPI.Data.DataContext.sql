IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517100200_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Email] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [PasswordHash] varbinary(max) NULL,
        [PasswordSalt] varbinary(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517100200_InitialCreate')
BEGIN
    CREATE TABLE [UserFavouriteMovies] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_UserFavouriteMovies] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserFavouriteMovies_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517100200_InitialCreate')
BEGIN
    CREATE TABLE [UserFollowers] (
        [FollowersId] int NOT NULL,
        [FollowedToId] int NOT NULL,
        CONSTRAINT [PK_UserFollowers] PRIMARY KEY ([FollowersId], [FollowedToId]),
        CONSTRAINT [FK_UserFollowers_Users_FollowedToId] FOREIGN KEY ([FollowedToId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserFollowers_Users_FollowersId] FOREIGN KEY ([FollowersId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517100200_InitialCreate')
BEGIN
    CREATE INDEX [IX_UserFavouriteMovies_UserId] ON [UserFavouriteMovies] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517100200_InitialCreate')
BEGIN
    CREATE INDEX [IX_UserFollowers_FollowedToId] ON [UserFollowers] ([FollowedToId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517100200_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210517100200_InitialCreate', N'3.1.3');
END;

GO


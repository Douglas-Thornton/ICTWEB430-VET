﻿using APP.Data.Models;

namespace APP.Services;

public interface IMeetingService
{
    public Task<List<Meeting>> GetMeetingsByInvitedUserId(int userId);
}


CREATE PROCEDURE [dbo].[spGetAllRegularTargets] 
	@userId varchar(100)
	AS
	BEGIN
		SET NOCOUNT ON;
	
		select 
			rt.Id
			, t.Name as TaskName
			, dTarget.Minutes as TargetDuration
			, dPerTime.Minutes as PerTimeframeDuration
			, rt.Created
			, rt.LastUpdated
			, rt.Deleted
		from 
			dbo.RegularTargets rt
			join dbo.Tasks t on rt.TaskId = t.Id
			join dbo.Groups g on t.GroupId = g.Id
			join dbo.Durations dTarget on rt.TargetDurationId = dTarget.Id
			join dbo.Durations dPerTime on rt.PerTimeframeDurationId = dPerTime.Id
		where 
			(t.UserId = @userId)
			and (rt.Deleted is null)
	END


CREATE OR ALTER PROCEDURE [dbo].[spGetAllRegularTargets] 
	@userId varchar(100)
	AS
	BEGIN
		SET NOCOUNT ON;
	
		select 
			rt.Id
			, t.Name as TaskName
			, TimeDedicatedToPerformTarget as TargetDuration
			, RegularTimeIntervalToAchieveTarget as PerTimeframeDuration
			, rt.Created
			, rt.LastUpdated
			, rt.Deleted
		from 
			dbo.RegularTargets rt
			join dbo.Tasks t on rt.TaskId = t.Id
			join dbo.Groups g on t.GroupId = g.Id
		where 
			(t.ApplicationUserId = @userId)
			and (rt.Deleted is null)
	END

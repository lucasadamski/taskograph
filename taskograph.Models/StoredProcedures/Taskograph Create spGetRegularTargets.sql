USE [Taskograph]
GO
/****** Object:  StoredProcedure [dbo].[spGetRegularTargets]    Script Date: 12-Apr-24 10:37:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE OR ALTER PROCEDURE [dbo].[spGetRegularTargets] 
	@userId varchar(100),
	@dateFrom date,
	@dateTo date
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
			and ((rt.Created >= @dateFrom) and (rt.Created <= @dateTo))
			and (rt.Deleted is null)
		

	END

USE [DB_A6A68A_Fas]

update ProjectTasks set GroupId=1 where TaskOrder<=16
update ProjectTasks set GroupId=2 where TaskOrder>=17 and TaskOrder<=24
update ProjectTasks set GroupId=3 where TaskOrder>=25 and TaskOrder<=31
update ProjectTasks set GroupId=4 where TaskOrder>=32 and TaskOrder<=34
 
 


SELECT      MIN(ProjectTasks.StartDateActual) AS StartDate, MAX(ProjectTasks.EndDateActual) AS EndDate,
			datediff(day,MIN(ProjectTasks.StartDateActual),MAX(ProjectTasks.EndDateActual)) totaltime , SUM(ProjectTasks.Duration  ) AS Duration, 
			SUM(ProjectTasks.Duration *100) AS actual, SUM(ProjectTasks.Compleation*ProjectTasks.Duration) AS CurrentCompleation,
			ProjectTasks.ProjectId, ProjectTasks.GroupId,Projects.ProjectName
FROM        ProjectTasks INNER JOIN
            Projects ON ProjectTasks.ProjectId = Projects.ProjectId
GROUP BY ProjectTasks.ProjectId, ProjectTasks.GroupId, Projects.ProjectName
ORDER BY ProjectTasks.ProjectId,ProjectTasks.GroupId

 
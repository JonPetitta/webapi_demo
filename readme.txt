Overview:
Build an API project that will support an application that would allow
users to login, create and manage tasks, using basic authentication.

Details:
The user will need to be able to login using a username and
password.

A task is defined as an event or action that would allow one to
enter a date and time to complete by and a description of the
event or action.

A user will be able to:
o Create a task with a date and time and description
o View all tasks created by the user
o Delete a task created by the user.
o Mark a task created by the user as complete.


DB Design:
CREATE TABLE `user` (
`id` int(10) unsigned NOT NULL AUTO_INCREMENT,
`firstname` varchar(150) NOT NULL,
`lastname` varchar(150) NOT NULL,
`username` varchar(150) NOT NULL,
`password` varchar(150) NOT NULL,
`insertdate` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `usertasks` (
`id` int(10) unsigned NOT NULL AUTO_INCREMENT,
`title` varchar(150) NOT NULL,
`completedate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP,
`userid` int(255) NOT NULL,
`taskcomplete` bit(1) NOT NULL,
`insertdate` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;
SET FOREIGN_KEY_CHECKS=1;
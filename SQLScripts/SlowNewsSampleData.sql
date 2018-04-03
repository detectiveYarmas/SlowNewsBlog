
/*********************************************************************************************
DO NOT PUSH TO PRODUCTION THIS IS FOR TESTING ONLY

RUNNING THIS SCRIPT WILL DELETE ALL DATA DROM THE DATABASE AND REPLACE IT WITH SAMPLE DATA

DO NOT PUSH TO PRODUCTION THIS IS FOR TESTING ONLY
***********************************************************************************************/

USE SlowNewsBlog
GO

DROP PROCEDURE IF EXISTS DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
DELETE FROM BlogPostsHashTags
DELETE FROM HashTags
DELETE FROM BlogPosts
DELETE FROM Catagories
DELETE FROM StaticPages

DBCC CHECKIDENT ('Catagories', RESEED, 1);
DBCC CHECKIDENT ('HashTags', RESEED, 1);
DBCC CHECKIDENT ('StaticPages', RESEED, 1);
DBCC CHECKIDENT ('BlogPosts', RESEED, 1);


SET IDENTITY_INSERT Catagories ON;
--catagories
INSERT INTO Catagories (CatagoryId, CatagoryName)
VALUES (1,'Politics'), (2,'Science'), (3,'Sports'), (4,'World'), (5,'Money'), (6,'Health'), (7,'Entertainment'), (8,'Style')

SET IDENTITY_INSERT Catagories OFF;

SET IDENTITY_INSERT HashTags ON;
--hashtags
INSERT INTO HashTags (HashTagId, HashTagName, Approved)
VALUES (1, '#metapost', 1), (2, '#horseandbuggy', 1), (3, '#moon', 0), (4, '#y2k', 0)

SET IDENTITY_INSERT HashTags OFF;

SET IDENTITY_INSERT StaticPages ON;
--staticpages
INSERT INTO StaticPages (StaticPageId, Title, Body)
VALUES 
(1, 'Welcome', '<p>Morbi vel commodo erat. Aenean commodo turpis eget bibendum posuere. Curabitur nec ligula lectus. Quisque pretium laoreet velit sed pharetra. In et sollicitudin elit. Mauris at ultrices turpis. Etiam pellentesque ante nibh, vitae tristique ipsum semper quis. Morbi metus nibh, gravida nec nisl sit amet, dictum dignissim lorem. Nulla pretium libero a est molestie volutpat. Fusce at feugiat ante. Etiam a enim eget magna egestas volutpat. Nunc at consectetur neque. Nam fringilla nulla arcu, ac lobortis nibh iaculis non. Quisque interdum tincidunt hendrerit.</p><p>Nam a massa at dui mollis pellentesque vitae eget quam. Fusce ornare eget lorem eget fringilla. Vivamus libero ex, consectetur nec consectetur quis, lacinia ut velit. Integer ac nibh laoreet lectus fringilla condimentum. Suspendisse molestie tortor ut sem vulputate tempor. Fusce ac sem ac sem scelerisque ullamcorper in et augue. Maecenas congue purus et augue mattis, et ornare quam mattis. Nulla semper purus a commodo mattis. Fusce urna lacus, interdum et ipsum id, vestibulum interdum tortor. Aliquam sed mollis leo, non tempus justo.</p><p>Etiam eu facilisis lacus, at facilisis nulla. Nam vitae gravida mi. Etiam a lorem erat. Sed at ornare leo. Nunc non eros vitae nibh semper rhoncus ut at justo. Pellentesque sodales auctor viverra. Phasellus vitae ullamcorper nulla. Integer venenatis lorem massa, a viverra sem aliquam a.</p><p>Aliquam eleifend, nunc ac aliquam semper, felis nisi feugiat sem, sit amet interdum mauris metus sed augue. Vestibulum non malesuada velit. Fusce ac auctor eros, nec condimentum risus. Sed non diam vestibulum, convallis nisl id, eleifend metus. Cras sollicitudin porta est, vel elementum arcu ultrices non. Vestibulum non scelerisque lorem, vel rutrum turpis. In eleifend leo ac euismod suscipit. Proin in nibh at elit ultrices ullamcorper. Quisque eleifend, tellus quis efficitur tristique, neque sem interdum massa, id elementum mi orci id justo. Phasellus at orci elit. Nunc sed sem condimentum, consectetur eros ut, auctor ipsum. Donec a quam tristique, pellentesque tortor non, tristique nisi. Vivamus at leo ut quam ultrices consequat.</p><p>Suspendisse erat mi, porta id facilisis et, pellentesque vel risus. Etiam tellus lacus, condimentum id justo ut, egestas convallis turpis. In porttitor nunc ac tempus ornare. Vestibulum facilisis lectus eros. Aenean mollis nibh enim, at hendrerit arcu posuere id. Nam erat tellus, tempus a gravida ut, imperdiet non nunc. Praesent feugiat, nisi ac ornare porta, augue felis maximus magna, at efficitur dolor nisl in libero. Maecenas porttitor, orci eget fringilla porttitor, urna mauris cursus erat, nec ullamcorper metus sem eu ipsum. Etiam consequat velit eu condimentum interdum. Suspendisse mattis ac ex non mattis. Phasellus ut nisl ultricies, venenatis libero at, iaculis urna. Curabitur ullamcorper vestibulum tortor vel congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;</p>'),
(2, 'Appologies', '<p>Nam rutrum aliquam consequat. Pellentesque quis <strong>tortor</strong> vitae neque pretium interdum. Maecenas pellentesque eget mauris ac bibendum. Cras feugiat tincidunt eros, et euismod felis malesuada id. Suspendisse imperdiet porttitor est, ac ultricies urna sagittis pharetra. Quisque vitae orci sit amet diam fermentum pretium. Ut et congue magna. Curabitur leo enim, aliquet a dignissim id, vestibulum non diam. Donec facilisis ullamcorper porta. Cras cursus libero eu dolor convallis pharetra. Pellentesque finibus commodo lacus, sit amet luctus lacus gravida eget.</p><p>Nulla ullamcorper massa vitae maximus fringilla. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla lacinia sapien ut sem faucibus ultrices. Proin et felis elementum neque hendrerit consectetur. Nunc non porta arcu. Nunc facilisis non augue a placerat. Suspendisse vel aliquam nisi. Donec in velit consequat, scelerisque metus quis, ornare ipsum. Maecenas at lobortis elit. Praesent placerat maximus mi. Donec non volutpat lacus. Cras rhoncus odio eget facilisis auctor. Integer venenatis orci eu cursus mattis.</p><p>Morbi condimentum elementum magna quis commodo. Nulla in lectus nisi. Duis scelerisque vulputate turpis dignissim eleifend. Nulla fringilla eget ligula ac ultricies. Maecenas maximus imperdiet ex, et auctor dolor pellentesque nec. Quisque cursus metus non elit pulvinar commodo. Nunc dui quam, luctus eget risus a, tristique tempus sapien. Aenean in semper mauris. Nulla facilisi. Curabitur ullamcorper lorem nibh, id porta nulla consectetur sit amet. Fusce at elit a ligula elementum tempus at eget ex. Nam eu pharetra enim, eget congue orci. Proin rutrum vel arcu ac ornare.</p>')

SET IDENTITY_INSERT StaticPages OFF;

SET IDENTITY_INSERT BlogPosts ON;
--blogposts
INSERT INTO BlogPosts (BlogPostId,PublishDate,Approved, Title, HeaderImage, Blog,CatagoryId, Id )
VALUES 
(1, '2018-01-01',1,'The Great Moon Escape', 'moon.jpg', '<p>Vivamus tortor felis, ornare ut tempus ac, imperdiet accumsan ante. Etiam ut porta leo, tempus euismod magna. Aliquam interdum rhoncus feugiat. Sed lacinia interdum sapien. Suspendisse viverra, ipsum non fringilla scelerisque, ipsum augue pellentesque est, nec vehicula quam augue nec urna. Nullam nulla turpis, imperdiet a porttitor vitae, tempus non nisi. Nulla non placerat nunc. Nam vitae orci sit amet ipsum blandit imperdiet et sit amet enim. Ut eget posuere massa, quis ultrices magna. Proin euismod ligula tempus mi pulvinar, non molestie lectus ornare. In dignissim lacus ligula, maximus efficitur nunc sodales sed. Proin ac ligula bibendum, porta arcu eget, commodo tortor. Nulla vitae magna semper lacus egestas cursus. Proin feugiat dignissim sapien, eget sagittis diam condimentum eu. Morbi porttitor augue vel ante varius placerat. Proin aliquam a odio sit amet sagittis.</p><p>Sed gravida dui ut nulla mollis, non malesuada massa feugiat. Etiam finibus odio ultricies mauris pretium, in dictum lorem aliquet. Phasellus vulputate sollicitudin tempor. Aliquam erat volutpat. Etiam tincidunt orci eu sapien aliquet, eu semper elit facilisis. Suspendisse potenti. Nulla facilisi. Mauris vel lectus ut ex porttitor lacinia at eget justo. Donec in odio varius, dapibus augue id, vehicula lorem. Suspendisse venenatis risus in magna dictum suscipit. Curabitur vestibulum leo volutpat, tempor nunc a, posuere mauris. Sed auctor justo non turpis tempus maximus. Phasellus arcu sapien, laoreet ut libero ut, mattis mollis ante. Sed scelerisque congue porttitor.</p><p>Vivamus accumsan, justo eget venenatis viverra, orci arcu efficitur purus, eu pellentesque diam lacus sit amet mauris. Pellentesque ut pharetra leo. Quisque odio purus, lacinia eu eros in, placerat tincidunt dolor. Proin magna eros, tempor vel nulla at, rutrum lacinia libero. Sed varius fringilla condimentum. Donec sit amet varius ex, nec pellentesque risus. Donec in tortor at purus dapibus aliquet.</p><p>Aliquam malesuada orci non lacus egestas fermentum. Sed aliquet, dui eu malesuada aliquam, nibh dolor hendrerit est, sit amet pretium lacus metus nec lectus. Cras eleifend velit non magna dapibus sagittis. Donec commodo malesuada tellus non commodo. Vivamus ac velit metus. Duis ac vestibulum leo. Vivamus cursus lorem felis, placerat accumsan metus sagittis id. Nulla neque neque, lacinia id iaculis placerat, ultrices nec enim. Curabitur porta volutpat massa, vitae lobortis ex malesuada suscipit. Duis rutrum, lacus ut auctor fringilla, sem nisl aliquet dui, quis feugiat libero ligula et ligula. Ut ex mi, rutrum eget vestibulum et, euismod eu augue. Maecenas dolor est, rutrum eu mattis dignissim, finibus vehicula odio. Suspendisse eget velit sit amet est tincidunt condimentum ut ac dui. Nullam tincidunt nulla elit, at efficitur turpis tristique id.</p><p>Quisque euismod mauris sit amet augue gravida, sed eleifend felis vestibulum. Cras vel dignissim arcu, sit amet ultricies tellus. Aenean imperdiet venenatis sapien ac eleifend. Praesent eu accumsan nisl, non pretium mi. Vivamus aliquam ligula non volutpat malesuada. In non elit eleifend, tempor turpis at, dictum lacus. Integer arcu dui, faucibus consectetur dui eu, dictum suscipit tellus. Praesent sit amet ante convallis, congue tortor ut, consequat erat. Sed non felis in diam pulvinar cursus. Cras molestie dui ut rhoncus sollicitudin. Donec posuere nulla sed nisl fermentum rutrum. Donec vitae rhoncus ex, at tempus felis.</p><p>Nunc non commodo eros. Quisque pellentesque mi vel magna dapibus ullamcorper elementum nec leo. Nunc accumsan lorem ipsum. Duis porttitor, nibh id laoreet euismod, felis diam rutrum neque, non viverra est erat eu ex. Mauris dictum velit a purus mattis rhoncus. Donec luctus malesuada tortor, sit amet blandit lectus accumsan at. Proin placerat vehicula leo, vel dapibus ligula dictum non. Maecenas sapien magna, venenatis eget dictum sagittis, maximus ac mauris. Aenean sit amet dictum justo. Sed nec egestas turpis, ac porta nibh. Integer ultrices orci sed lacus consectetur faucibus. Suspendisse nec neque at justo malesuada euismod ut ut arcu. Vestibulum finibus lacinia nisi. Fusce consequat tellus ut ipsum euismod, eu scelerisque lacus lacinia. In placerat nisl vel lorem euismod consequat.</p><p>Etiam tempor volutpat turpis, ut vulputate magna eleifend ut. Duis felis ipsum, pulvinar ac ligula eget, semper lacinia turpis. Pellentesque tincidunt risus et sapien pellentesque rutrum. Nunc eu libero porta, posuere ex ut, ornare dolor. Vivamus non interdum velit, in elementum eros. Quisque vitae laoreet nisi, et vestibulum orci. In sit amet tincidunt tellus, nec tincidunt lorem. Fusce at nisl id massa ultricies congue. Proin elit leo, pretium nec lobortis ac, fermentum sit amet est. Maecenas nec ultrices nulla, sit amet mattis nunc.</p><p>Fusce et tempor sapien, et pretium neque. Donec laoreet vulputate mollis. Donec ipsum tellus, pulvinar ut sapien nec, tincidunt ultrices dui. Vestibulum in neque arcu. Aliquam tincidunt odio eget lectus accumsan, eu commodo quam mattis. Integer ultricies enim at ex ullamcorper vulputate. Suspendisse potenti. Phasellus eu molestie purus. Donec non viverra elit. Sed gravida imperdiet volutpat. Phasellus ornare rutrum nisi vel tincidunt.</p>',1,'8db40059-9aad-43b9-bc79-575ee5ec3dce'),
(2, null,1,'Did You Hear About Pogs?', 'pogs-and-slammers.jpg', '<p>Mauris consequat convallis nibh convallis pellentesque. Cras lobortis augue ac nunc bibendum, a laoreet velit rutrum. Nunc semper tincidunt nulla nec faucibus. Integer vitae mauris malesuada, viverra mi vel, tempor felis. Morbi id risus iaculis, luctus purus et, lobortis libero. Nam elementum, dui vehicula porta sodales, sem augue congue augue, ultrices imperdiet urna nulla sit amet ipsum. Integer vestibulum interdum tellus. Quisque rhoncus sem ac ante imperdiet luctus.</p><p>Maecenas feugiat, enim quis iaculis interdum, est diam pulvinar odio, id malesuada dolor felis sit amet libero. Suspendisse accumsan viverra imperdiet. Morbi pretium ac sem ac scelerisque. Aliquam nibh diam, porta ut gravida eget, volutpat non lacus. Vivamus viverra laoreet sem nec blandit. Maecenas nec fermentum erat. Aliquam erat volutpat. Aenean hendrerit id elit eget finibus. Quisque pellentesque lorem sit amet massa cursus, fermentum pulvinar risus posuere. Vestibulum malesuada, urna elementum sodales molestie, erat lacus ultrices eros, in condimentum neque justo non lectus. Suspendisse nec tempor lacus, a semper purus. Vivamus sed blandit lacus, id semper felis. Fusce aliquet, lectus et ultrices feugiat, urna mauris laoreet felis, ac commodo nibh diam sit amet ipsum.</p><p>Proin imperdiet ante nec nisl feugiat pharetra. Morbi volutpat, ligula nec facilisis consectetur, lectus nisi lobortis urna, eget volutpat magna felis at elit. Integer sollicitudin accumsan ipsum, sed vestibulum dui volutpat et. In vestibulum tellus et imperdiet tempus. Integer non rhoncus est, nec porttitor neque. Aenean a orci a odio volutpat ultricies ac sit amet est. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse blandit ligula sit amet leo varius, et vestibulum arcu molestie.</p><p>Curabitur a lorem ut ligula pulvinar gravida. Donec fermentum orci et sem rhoncus, et iaculis tellus egestas. Pellentesque sed ipsum felis. Mauris nibh erat, condimentum non pellentesque pellentesque, pulvinar eu quam. Etiam aliquet urna non orci mattis tristique. Etiam varius fermentum faucibus. Proin egestas laoreet convallis. Proin aliquam risus vel ante auctor, sit amet tincidunt lectus laoreet. Suspendisse non nulla eget mauris iaculis venenatis. Mauris facilisis semper rutrum. Donec dapibus cursus elementum. Etiam consequat, metus euismod semper placerat, metus arcu rhoncus est, quis lobortis dolor felis at justo. Vestibulum dapibus nec nisl ac eleifend. Curabitur non magna eget felis consectetur malesuada. In viverra arcu ac nunc ornare, eu accumsan magna consectetur. Duis porta, lectus nec venenatis volutpat, mi nibh porta dolor, eget bibendum ipsum mauris ut sem.</p><p>Sed dignissim tellus id lectus volutpat aliquam quis vitae mi. Morbi elit tortor, viverra a iaculis at, dapibus vitae lacus. Vivamus in placerat eros. Quisque odio ex, convallis ac velit vitae, semper mattis leo. Morbi eleifend, urna at vulputate blandit, tellus nulla tempor leo, at suscipit risus enim vel augue. Curabitur accumsan lorem nulla, at volutpat dolor varius eu. Cras a convallis tellus. Morbi quis ante a elit faucibus cursus. Mauris id egestas enim. Vivamus in mi enim. Duis ac ipsum vitae dui feugiat consequat et et est. Mauris et lacus eget neque elementum fermentum.</p><p>Ut nec quam dapibus, consequat magna eget, scelerisque lacus. Nullam gravida turpis vitae urna mollis, quis auctor nisl porta. Donec ultrices libero sed velit pellentesque, in aliquet augue vulputate. Cras blandit, neque ac imperdiet accumsan, dolor risus egestas nibh, quis luctus turpis tortor sit amet felis. Nullam quis feugiat sapien. Aliquam ultricies pharetra interdum. Phasellus mattis massa nisl, eu hendrerit lectus tempor vel. Sed varius efficitur mauris, et ultrices ex euismod consequat. Proin ut nulla a nisl efficitur congue. Vestibulum euismod turpis at nunc volutpat finibus. Sed aliquet eu mi eu posuere. Aliquam erat volutpat. Maecenas et viverra massa. <strong>Integer</strong> consectetur neque quis justo laoreet consequat semper a mi.</p><p>Cras tempor bibendum nulla, varius convallis elit euismod quis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Suspendisse tempus tincidunt lacus, sed dignissim nulla sagittis in. Duis nec egestas sapien. Sed auctor ligula lacus, at laoreet nulla lobortis non. Nam facilisis, libero nec mattis volutpat, libero est pharetra eros, ut dignissim odio mauris et nulla. Praesent bibendum, libero sed molestie aliquam, arcu risus varius lectus, sed vulputate velit nulla eu mi. Cras vitae sapien in dui facilisis laoreet quis quis turpis. Nunc gravida metus purus, pulvinar rhoncus justo pretium et. Praesent tempus fermentum magna sit amet sollicitudin. Integer dolor est, porttitor at sapien at, facilisis vestibulum elit. Integer malesuada nisi eget sem aliquet, vehicula consectetur sapien tristique.</p><p>Maecenas mauris augue, tincidunt non eros a, sagittis sagittis velit. Aliquam enim justo, placerat ac magna eget, tempor sagittis metus. Suspendisse maximus tellus tellus, non dapibus purus ornare ac. Nunc vestibulum non sapien quis pulvinar. Nullam faucibus risus sed leo rhoncus, at rhoncus nisl tempus. Proin justo mauris, mattis ac purus vel, luctus pretium velit. Etiam eget diam laoreet nibh semper sodales. In a porta augue. In sollicitudin nibh ligula, id gravida sem posuere ac. Phasellus volutpat ipsum ut neque suscipit eleifend. Phasellus volutpat rutrum tortor, sit amet iaculis nunc. Integer ex ante, luctus non sagittis ac, mollis eu ligula. Curabitur et tellus eu ligula efficitur elementum. In hac habitasse platea dictumst. Nulla eu semper velit.</p>',2,'8db40059-9aad-43b9-bc79-575ee5ec3dce')

SET IDENTITY_INSERT BlogPosts OFF;

--blogpostshashtags
INSERT INTO BlogPostsHashTags (BlogPostId, HashTagId)
VALUES
((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = 1),(SELECT HashTagId FROM HashTags WHERE HashTagId = 3)), 
((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = 2),(SELECT HashTagId FROM HashTags WHERE HashTagId = 3)),
((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = 1),(SELECT HashTagId FROM HashTags WHERE HashTagId = 4)),
((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = 2),(SELECT HashTagId FROM HashTags WHERE HashTagId = 1)),
((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = 2),(SELECT HashTagId FROM HashTags WHERE HashTagId = 2)),
((SELECT BlogPostId FROM BlogPosts WHERE BlogPostId = 1),(SELECT HashTagId FROM HashTags WHERE HashTagId = 1))



END
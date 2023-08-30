# ARH
Application Ressources Humaines.

## Objectifs
ct : court terme
mt : moyen terme
lt : long terme
- [ct] Permettre à chaque personne de saisir ses jours de présence/absence dans les locaux/en télétravail et si absence de la préciser (CP, CSS, etc.)
- [ct] Permettre au personnel administratif (comment les identifier ? à définir) et aux managers (idem) de voir les saisies des autres 
- [ct] Les jours fériés peuvent être saisis dans un panneau d'administration année par année. 
- [ct] Tout doit être exportable dans un format portable (xlsx ou csv probablement)
- [ct-mt] Les cas particuliers doivent être gérés (les prestataires ne signalent que les présences par ex ; un employé à mi-temps ne doit pouvoir saisir que sur ses horaires de travail...) 
- [mt] Les jours fériés doivent être calculés autant que possible pour ne pas avoir à les resaisir chaque année 
- [mt] Il doit être possible de définir des jours fériés particuliers (les jours monégasques, les jours d'Alsace, d'autres pays, etc.) et de dire à quels utilisateurs ils s'appliquent.
- [mt] Les jours fériés présents pendant un mois doivent être nommés (ex : lorsqu'on passe la souris sur le 25 décembre, le nom "Noël" doit s'afficher en tooltip)
- [mt] Permettre de consulter les congés de tous les salariés, savoir qui est en vacances quand, qui a combien de jours disponibles, et surtout vérifier les congés simultanés de plusieurs salariés pour s'assurer qu'il y a toujours un responsable à chaque pôle présent.
- [mt] Donc un panneau d'administration technique doit exister et permettre de les saisir. Et bien sûr son accès doit être restreint (gestion de droits via un rôle Active Directory ?).
- [mt] Idéalement toutes les actions doivent être tracées
- [lt] Une page doit permettre la consultation de ces traces avec un moteur de recherche
- [lt] Pour les managers, permettre une gestion par équipe (chaque manager ne devrait pouvoir voir que son équipe et pas les autres)

## Technique
- appli simple d'utilisation
- aspnet razor pages déployable sur IIS
- reliée à une base de données disponible en réseau local (sqlserver ou sqlite) qui doit être sauvegardée régulièrement et automatiquement (en cas d'accident on doit pouvoir remonter la base sans condition)
- certains champs doivent être préremplis par utilisateur
- connexion active directory (déjà en place à date d'écriture de ce document) et autorisation par rôles et au besoin users précis
- prévoir une appli par site avec synchro des bases de données
- l'appli ne doit pas être livrée en mode expérimental ==> prévoir des analyseurs de code, des tests, du coverage, etc.

## Type de rendu de base 

Jean Dupont pour le mois d'avril 2023

|  | lun | mar | mer | jeu | ven |
|--:|:-----:|:-----:|:-----:|:-----:|:-----:|
| |3|4|5|6|7|
|sur site|1|1|1|0,5|1|
|télétravail|0|0|0|0,5|0|
|déplacement professionnel|0|0|0|0|0|
|absence payée|0|0|0|0|0|
|absence non payée|0|0|0|0|0|

Commentaire : 
Télétravail pour suivi travaux à la maison

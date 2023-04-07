# ARH
Application Ressources Humaines.

## Objectifs
ct : court terme
mt : moyen terme
lt : long terme
- [ct] Permettre à chaque personne de saisir ses jours de présence/absence dans les locaux/en télétravail et si absence de la préciser (CP, CSS, etc.)
- [ct] Permettre au personnel administratif (comment les identifier ? à définir) et aux managers (idem) de voir les saisies des autres 
- [ct] Tout doit être exportable dans un format portable (xlsx ou csv probablement)
- [ct-mt] Les cas particuliers doivent être gérés (les prestataires ne signalent que les présences par ex ; un employé à mi-temps ne doit pouvoir saisir que sur ses horaires de travail...) 
- [mt] donc un panneau d'administration technique doit exister et permettre de les saisir. Et bien sûr son accès doit être restreint.
- [mt] idéalement toutes les actions doivent être tracées
- [lt] une page doit permettre la consultation de ces traces avec un moteur de recherche
- [lt] pour les managers, permettre une gestion par équipe (chaque manager ne devrait pouvoir voir que son équipe et pas les autres)

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
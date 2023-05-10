# ARH
Application Ressources Humaines.

## Objectifs
ct : court terme
mt : moyen terme
lt : long terme
- [ct] Permettre à chaque personne de saisir ses jours de présence/absence dans les locaux/en télétravail et si absence de la préciser (CP, CSS, etc.)
- [ct] Permettre au personnel administratif (comment les identifier ? à définir) et aux managers (idem) de voir les saisies des autres 
- [ct] Permettre la saisie des demandes de congés et leur validation par le manager, voire deux validations, celle du manager direct et celle de la direction
- [ct] Ajouter un calendrier annuel mettant en avant les congés et les absences, en distinguant par un code couleur ceux déjà passés et ceux à venir
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

## précisions sur le métier

### saisie mensuelle individuelle

- Une saisie correspond au temps passé sur site, en télétravail, en déplacement, en congés ou autres absences, payées ou non. 
- Une saisie est en portion de journée (0, 0.25, 0.5, 0.75 ou 1).
- Une saisie mensuelle est valide et normale si et seulement pour chaque jour ouvré (= qui devrait être travaillé, ex : du lundi au vendredi) la somme des valeurs saisies est égale à 1 et si le nombre total de jour saisis est bien égal au nombre de jours ouvrés. Par exemple, si j'ai 20 jours dans un mois, le total du mois doit être égal à 20 jours.
- Si le total d'un jour est inférieur à 1 on doit l'afficher en rouge clair (saisie incomplète)
- Si le total d'un jour est supérieur à 1 on doit l'afficher en rouge foncé (saisie probablement erronée)
- Si le total d'une ligne ou le total du mois est inférieur au nombre de jours ouvrés du mois on doit l'afficher en rouge clair (saisie incomplète)
- Si le total d'une ligne ou le total du mois est supérieur au nombre de jours ouvrés du mois on doit l'afficher en rouge foncé (saisie probablement erronée)
- Une saisie probablement erronée doit faire l'objet d'un commentaire. La saisie doit tout de même être permise mais un message doit s'afficher sur la page indiquant qu'il y a probablement une erreur et qu'il faut vérifier les totaux en rouge et en préciser la raison.

# Clone-Clash-Royale
Repository du clone du jeu clash royale pour l'exercice technique pour Go to Games. 
Au lieu de rendre un PDF contenant les informations ainsi que le build du jeu, je me suis permis de créer ce README qui reprend tous les points du développement.
Je vais donc reprendre point par point les difficultés, les solutions techniques que j'ai choisis et les pistes d'améliorations que j'aurais pu implémenter suivant les consignes du document PDF fourni.

## Map
Pas de difficultés lors de la création de la map. J'ai pu donc reprendre le design donné dans le PDF original et créer une map simple avec des carrés et des plans. 
En utilisant des matériaux simples et colorés j'ai pu donner quelques dégradés sur cette map.  
Pour améliorer cette carte, avec le temps, il aurait été simple de rajouter des objets 3D ou des matériaux de plus haute qualité.

## Unité
Pour la création des unités, le plus complexe, mais qui n'est pas de mon ressort, était de faire en sorte que les unités soient toutes équilibrées.
En effet, lors de mes tests, j'ai rapidement pu voir que certaines de ces unités étaient beaucoup trop puissantes et ne permettaient pas de créer un "jeu" amusant.  
Les points de difficulté concernant les unités concernent plus leur déplacement et leurs attaques plus que la création en elle-même.
Il y a 8 unités comme demandées :  
- Le ballon (rouge), comme dans Clash Royale le ballon est une unité volante (elle ne suit donc pas le chemin) qui n'attaque pas les autres unités et qui attaque lentement mais de façon très forte  
- Le mage (bleu clair), le mage est une unité au sol avec peu de vie qui va faire de puissants dégats sans attaquer très rapidement  
- Le barbare (orange), le barbare est une unité moyenne, elle se déplace à vitesse moyenne, fait des dégats moyens et possède des points de vie moyens  
- Les squelettes (blanc), unité au sol qui fait apparaître 5 unités, elles ont peu de vie et peu d'attaque mais se déplacent relativement vite  
- Le rat (marron), première unité non issue de Clash Royale, une unité rapide et très fragile qui ne coûte pas beaucoup de mana  
- Le boulder (gris), deuxième unité non issue de Clash Royale (fortement inspirée du géant), une unité lente qui ne s'intéresse pas aux autres unités mais qui possède beaucoup de vie et qui fait beaucoup de dégats aux bâtiments  
- Les serviteurs / imp (violets), unité volante composée de 4 unités, ces unités sont rapides mais fragiles et font des dégats moyens  
- Les serpents (vert), troisième unité non issue de Clash Royale, les serpents apparaissent en duo et font de gros dégats tout en allant vite, cependant leur vie est très basse  
Pour améliorer ce fonctionnement d'unité, il aurait été simple de travailler avec un game designer pour mieux les équilibrer et surtout avec un artiste pour avoir des modèles 3D plus convaincants.

## Mana
Pour la création du mana, il n'y a pas eu de point de difficulté. En utilisant une coroutine par joueur qui tous les 4 secondes rajoute 0.25 de mana. 
J'aurais pu bien évidemment utiliser une seule coroutine d'un objet plus général qui donne aux deux joueurs en même temps.
Cependant, pour plus de lisibilité et pour moins compléxifier le code j'ai préféré opter pour deux coroutines.  
L'utilisation du composant Slider d'Unity m'a permis de façon simple d'afficher la quantité de mana pour chacun des joueurs.  
De plus, en utilisant une classe qui va connait toutes les cartes du jeu par joueur (la classe Deck), je pouvais facilement mettre à jour les cartes du deck à chaque mise à jour du compte de mana.
Pour améliorer le fonctionnement du mana, j'aurais pu utiliser un format type événement / abonnement. Chaque objet ayant besoin de mettre à jour quelque chose lorsque le mana est mis à jour aurait pu se brancher à ce circuit et ainsi ne pas avoir d'appels entre les classes.

## Deck
Pour la création du deck, j'ai pu utiliser une classe qui connaissait toutes les unités et les composants d'UI d'Unity.
Grâce à des cartes pré-créées au sein de l'éditeur Unity je pouvais dire au jeu qu'il y avait 8 cartes pour l'adversaire et 4 cartes pour nous.  
Ces cartes, comme précisé plus haut, se mettaient à jour dès que le mana était ajouté ou enlevé.  
Le drag and drop n'a pas été ajouté comme feature car j'utilisais des Grid Layout pour l'affichage des différentes cartes. De plus je ne savais plus si dans le jeu de base c'était un élément de l'interface qui était drag and drop ou si c'était un object 3D créé lors du clic sur la carte.
Je n'étais pas certain de la façon à utiliser pour réaliser cette fonctionnalité dans les temps impartis.
Pour améliorer le fonctionnement du deck, j'aurais notamment pu ajouter des animations ou des images symbolisant les différentes unités. De plus, j'aurais pu faire apparaître les cartes selon un paramètre afin de rendre dynamique le nombre de cartes qui peuvent apparaître.  

## Spawn d'unité
Pour créer une unité facilement j'ai pu créer un collider sur le monde directement. C'est un collider qui s'affiche lorsqu'on clique sur une carte.
Puis lorsque la carte est sélectionnée et que le joueur clique sur le collider, un raycast part de la caméra pour aller vérifier l'endroit précis où le joueur a cliqué.  
Une fois cela fait, j'utilise un système de pools pour réutiliser les mêmes assets plusieurs fois au lieu de les détruire à chaque fois. Les seules exceptions sont les tours, déjà présentes lors du chargement.  
L'unité est ensuite placé dans le monde au point d'impact, puis le collider ainsi que la carte utilisée sont désactivés.
Pour le spawn des unités ennemies, j'ai délimité un carré et lorsque l'ennemi a le mana pour faire apparaître une unité il choisit à l'intérieur de ce carré aléatoirement.

## Déplacement normal d'unité
Au lieu d'expliquer le déplacement d'unité j'ai pu le mettre en place. En utilisant un système de "waypoints" très simple (chaque point connaissant le précédent et le prochain), j'ai pu ajouter ce "pathfinding" très basique.
Il y a 7 points par chemin et il y a 2 chemins (un chemin par le pont de droite et un par le gauche), lorsqu'une unité apparaît, elle va chercher le point le plus proche et essayer de s'y rendre.  
Si elle arrive à s'y rendre (sans combat entre temps), alors elle ira au prochain point en suivant une ligne (ou précédent si l'unité est ennemie).  
J'aurais pu modifier et améliorer ce système de bien des manières :
- Utiliser des courbes de Bézier pour faire des courbes et des arrondis au lieu de ces lignes peu réalistes.
- Utiliser plus de waypoints pour ajouter de nouveaux trajets (par exemple en plaçant 2 chemins "extrêmes" les unités auraient créées elles-mêmes leur chemin basé entre ces deux chemins.  
- Utiliser un vrai système de pathfinding comme celui d'Unity en ajouter les unités en tant qu'Agent et en utilisant une NavMesh. Cela aurait permis, en plus de faciliter la tâche, d'ajouter du relief ou des sauts.
- Utiliser un autre système de pathfinding comme Djikstra ou A* mais fait maison.

De plus, comme on peut le voir, sans système de pathfinding ou sans waypoints nombreux, il arrive assez régulièrement qu'une unité marche dans l'eau, cela aurait pu être évité en ajoutant des colliders invisibles au dessus des zones d'eau.  
Cependant, comme on peut le voir dans le clone, j'ai dû jongler avec de nombreuses fonctionnalités pour rendre cela utilisable.
En effet, sans navmesh, les agents pouvaient s'entasser et on ne voyait pas facilement les différentes unités. 
J'ai pu donc ajouter des RigidBodies et des Colliders pour modifier le tout. Cependant, les RigidBodies ne sont pas aussi précis que des Transform, au lieu donc de regarder précisèment leur position par rapport au point j'ai regardé une différence de magnitude.

## Déplacement de combat d'unité
Ici aussi le déplacement de combat des unités a été codé.  
Pour détecter les autres unités, chaque unité possède deux colliders en forme de sphère, la première sert à "voir" un ennemi et se diriger vers lui alors que la deuxième sert à attaquer les autres unités.  
Dès qu'une unité rentre dans le premier cercle alors notre unité va se diriger vers elle.
Lorsque l'unité entre dans le second cercle, notre unité va commencer à l'attaquer. 
Pour améliorer le système de déplacement, j'aurais pu mieux gérer les différents cas pour ne pas avoir ce léger décalage lorsqu'une unité change de cible et ainsi avoir une expérience plus fluide.

## Combat
Ici aussi le combat à été programmé.  
Lorsqu'une unité combat une autre elle va faire apparaître des projectiles qui vont se diriger vers l'unité.  
Lorsqu'un projectile touche une unité ennemie, celle-ci prend des dégats.  
Comme précédemment, j'ai pu utiliser un système de pool pour stocker les projectiles déjà utilisés et les réutiliser plus tard au lieu de les faire disparaître.  
Pour améliorer le combat j'aurais pu utiliser différents types de projectiles pour rendre le jeu plus amusant, comme par exemple des projectiles explosifs ou encore des animations et / ou particules.  
Mais une autre façon d'améliorer ce système aurait été de disposer de 4 sphères au lieu de 2.  
En effet, lors de tests j'ai pu remarquer que beaucoup d'unités ne suivent pas assez rapidement les autres, ou le temps d'attaque et trop long et bien d'autres cas encore.
En utilisant 2 fois plus de sphères on peut dans ce cas avoir une sphère qui va permettre à la cible de rentrer tandis que l'autre permettra à la cible de sortir.  
Pourquoi deux sphères quand une peut suffir ?  
Car en ayant une sphère d'entrée plus grande que la sphère de sortie, on obtient une zone buffer qui nous permet des fluctuations plus faciles et moins impactantes.
Une unité peut ainsi avoir le temps d'effectuer une animation et d'avoir un délai avant de devoir suivre l'autre unité et la perdre de vue.

## Tour
Le système de tour a lui aussi été programmé.  
Lorsqu'une unité rentre dans le cercle d'attaque de la tour (la tour ne possède qu'un cercle comme elle ne peut pas bouger le deuxième cercle pour "voir" ne lui sert à rien), elle reçoit des projectiles lents mais très puissants.
La tour est également utilisée dans l'apparition des monstres volants, qui au lieu de suivre le chemin se repère grâce aux tours.  
Lorsque la dernière tour, le donjon, est détruite, le jeu s'arrête et c'est l'adversaire qui remporte.
Pour améliorer le système de tours, il aurait été possible d'ajouter un système de prioritisation sur les cibles pour d'abord taper les plus dangereuses en premier.  
En général, un système de prioritisation pour les projectiles aurait permis aux unités de ne pas taper des unités qui sont sur le point de mourir, afin de ne pas perdre du temps à attaquer une unité bientôt détruite.

## IA
L'IA adverse est très basique, au début du jeu elle choisit aléatoirement une carte parmis les 8 possibles et va attendre qu'elle possède assez de mana pour l'utiliser.
Cette IA est mise à jour toutes les secondes pour vérifier si elle possède assez de mana pour invoquer cette unité. Lorsque l'unité est invoquée aléatoirement sur son terrain, l'IA passe à une autre unité.
Pour améliorer l'IA facilement de nombreuses possibilités sont à notre disposition :
- Ajouter une priorisation gauche / droite selon les dégats déjà fait au tour ou aux ennemis arrivant
- Ajouter des modes de jeu (agressif ou défensif par exemple) pour contrer nos attaques
- Avoir une connaissance de nos ressources et de nos possibilités pour mieux nous contrer.

Merci beaucoup d'avoir lu jusqu'ici, si vous avez d'autres questions n'hésitez pas.

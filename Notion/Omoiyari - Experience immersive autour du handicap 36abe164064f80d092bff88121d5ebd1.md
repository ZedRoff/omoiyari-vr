# Omoiyari - Experience immersive autour du handicap (VR version)

**Étudiants :**

- Justine **HAKIM**
- Aman **GHAZANFAR**

**Filière :** E4-FI

**Unité :** eXtented reality (UE Projet multidisciplinaire 4)

**Année universitaire :** 2025–2026

**École :** ESIEE

## 0. Préambule

---

Dans le cadre de l'unité d'**Infographie 3D** de la filière Informatique à l'**ESIEE Paris**, ce rapport documente le cycle de conception et de développement du projet **OMOIYARI [思いやり] en VR**.

Ce projet s'inscrit dans une démarche d'ingénierie et de recherche logicielle visant à explorer les capacités de la Réalité Virtuelle (VR) non plus seulement comme un vecteur de divertissement, mais comme un puissant outil de simulation, de sensibilisation et d'empathie active. En transposant un prototype initialement développé pour une configuration classique clavier-souris vers un environnement spatialisé et immersif à 360° sur Meta Quest 2, notre binôme a été confronté aux défis majeurs de l'optimisation mobile et à la cinétose (*cybersickness*).

Ce document, pensé en synergie avec notre [**GDD Dashboard Interactif**](https://zedroff.github.io/omoiyari-vr/), retrace nos choix philosophiques (notamment autour du concept du *Gaman*), l'application de la théorie des I2, le séquençage technique de nos 5 capsules sensorielles, ainsi que la logistique de nos pipelines de production.

Ce tableau de bord web interactif a été conçu comme un visuel de notre document de game design (GDD). Développé selon une esthétique inspirée de la charte graphique japonaise du projet, il centralise l'intégralité des spécifications techniques et ergonomiques de l'application. Cet outil offre une cartographie claire, synthétique et immédiate de l'architecture de notre univers virtuel et de son séquenceur de jeu.

## 1. Introduction et objectif du projet

---

### 1.1 Contexte et Genèse du Projet

Le projet **OMOIYARI** est né d'un constat critique : dans notre société contemporaine, la sensibilisation aux différents types de handicaps (moteurs, sensoriels ou cognitifs) reste trop souvent théorique, passive et purement informative. Les campagnes de sensibilisation traditionnelles s'appuient majoritairement sur des supports peu marquants (vidéos, brochures, conférences) qui échouent à faire ressentir la réalité concrète et quotidienne des barrières sociales et psychologiques.

Au commencement de cette unité, nous disposions d’un prototype de jeu complet et fonctionnel développé pour une configuration classique sur ordinateur (navigation au clavier et à la souris). Bien que ce système permettait de structurer l'expérience de base, l'utilisation de l'écran 2D instaurait une distance physique majeure. L'objectif central de notre travail a donc été d'opérer une transition en transposant ce prototype PC vers un environnement immersif en Réalité Virtuelle (VR) à 360° sur Meta Quest 2.

### 1.2 L'essence d'OMOIYARI : Incarner plutôt que regarder

Le terme japonais *Omoiyari* (思いやり) désigne un concept culturel profond lié à l'empathie, la bienveillance et la considération des besoins d'autrui. Fidèle à cette philosophie, notre projet redéfinit l'expérience utilisateur : l'enjeu n'est plus seulement de voir le handicap sur un écran, mais de l'incarner physiquement.

En plaçant le joueur dans la peau de Sagaeru et sous la direction narrative du personnage de Satoshi, l'expérience prend la forme d'un parcours à travers 5 capsules sensorielles et cognitives :

1. **La mobilité réduite** (parcours mécanique en fauteuil roulant).
2. **Le daltonisme** (altérations chromatiques en temps réel).
3. **Le spectre autistique** (crises de surcharge sensorielle et inversion des repères).
4. **La dyslexie** (distorsion textuelle mouvante et surcharge cognitive).
5. **Le retour d'expérience (REX)** (analyse pédagogique et quiz final).

### 1.3 Objectifs et Positionnement UX

L'intention première d'OMOIYARI n'est pas de proposer un produit de divertissement ludique traditionnel, mais d'utiliser les codes du jeu vidéo (game design, boucles de récompenses, scénarisation) au service d'une **simulation.**

L'expérience se positionne sur trois objectifs UX fondamentaux :

- **Faire naître une empathie active :** Utiliser la frustration mécanique engendrée par la VR comme un levier d'apprentissage. Le joueur subit les contraintes matérielles pour mieux comprendre le poids de l'effort quotidien, transformant la frustration en une prise de conscience à chaque résolution de salle.
- **Proposer un gameplay immersif et spatialisé :** Supprimer les abstractions du clavier pour les remplacer par des actions physiques concrètes (pousser les roues d'un fauteuil, isoler la lumière avec ses mains, manipuler des fioles de chimie via des mouvements réels).
- **Garantir l'accessibilité technique :** Développer une application fluide, stable et  optimisée (maintien d'une latence inférieure à 20ms sur Meta Quest 2) afin d'éliminer tout inconfort physique et d'ouvrir l'expérience à tous les profils de joueurs, des consultants experts aux novices de la technologie virtuelle.

---

## 2. Spécifications Techniques & Implémentation VR

### **a. Développement et Préparation des Scènes**

La création de l'univers a nécessité une phase de préparation et d'assemblage des scènes sous Unity 6000 afin d'assurer une cohérence parfaite entre la fidélité visuelle de l'univers japonais et les contraintes techniques de la réalité virtuelle. Notre démarche s'articule autour de deux axes méthodologiques majeurs :

1. **Évaluation de la précision et de la pertinence des modèles 3D intégrés :**
    
    Pour susciter une empathie authentique, chaque salle devait posséder un haut niveau de pertinence. Nous avons sélectionné et évalué nos modèles 3D selon des critères stricts de proportionnalité à l'échelle humaine (1:1), indispensable pour éviter tout sentiment de distorsion spatiale en VR.
    
    - *Pertinence thématique :* Dans le laboratoire de chimie (*Capsule Dyslexie*), les modèles de béchers, de fioles et le tableau ont été choisis pour leur aspect immédiatement reconnaissable, permettant de focaliser toute l'attention du joueur sur la perturbation cognitive des textes et non sur la compréhension des objets. De même, la structure d'aire de jeux a été implantée dans la *Capsule Daltonisme* pour réactiver des repères de l'enfance, rendant l'altération chromatique d'autant plus frappante.
    - *Précision géométrique et optimisation :* Tous les modèles intégrés (des pavillons traditionnels aux dalles du labyrinthe de l'autisme) ont été évalués pour leur densité polygonale (*Low-Poly* stylisé). Cette économie de polygones garantit que le processeur du Meta Quest 2 puisse rafraîchir l'affichage sans saccade, préservant ainsi le confort visuel du joueur. Les arbres utilisent un LOD bas, certains environnements sont générés de manière procédurale.
2. **Méthodologie pour l'intégration des éléments 3D dans un contexte de réalité virtuelle :**
    - *Mise à l'échelle et physique (Colliders & Rigidbodies) :* Chaque modèle destiné à être manipulé (comme la torche de la capsule motrice ou les pièces du puzzle Tangram de la capsule daltonisme) a été doté de *Mesh Colliders* simplifiés ou de *Box Colliders* précis. Cette étape est cruciale pour que la main virtuelle du joueur ne traverse pas les objets et que les collisions spatiales soient gérées de manière réaliste.
    - *Ancrage spatial et hiérarchisation XR :* L'intégration a été pensée en fonction de la position de la caméra de l'utilisateur (*XR Origin*). Par exemple, pour le fauteuil roulant de la *Capsule 1*, le modèle 3D du châssis a été parenté au repère de locomotion de la caméra. Cela garantit que lorsque le joueur se déplace, le modèle du fauteuil et la représentation visuelle de ses pieds restent fixes dans son champ de vision à 180°, appliquant ainsi notre stratégie de point d'ancrage visuel contre le *cybersickness*.
    - *Baking des textures et gestion de l'environnement :* Pour intégrer de grands éléments comme le Mont Fuji (sculpté directement avec l'outil *Terrain* de Unity) ou encore le Playground, nous avons appliqué un processus de baking des lumières. Les ombres et lumières de ces modèles 3D ont été baked dans les textures pour que l'intégration environnementale soit visuellement riche sans impacter la fluidité du build final.

### b. Interaction et Interactivité

L'architecture interactive a été entièrement repensée pour s'affranchir des abstractions du clavier et proposer une ergonomie spatiale, significative et intuitive, articulée autour de deux composants majeurs du package *Unity XR Interaction Toolkit* :

1. **Conception d'interactions utilisateur significatives :** Pour ancrer le joueur dans la réalité concrète des situations simulées, chaque action clé requiert une manipulation physique naturelle. La saisie et l'utilisation des objets environnementaux (tels que la torche pour embraser les porte feus dans la *Capsule 1* ou les fioles de solution dans le laboratoire de la *Capsule 4*) sont gérées via des composants **XR Grab Interactor**, assignés au bouton *Trigger* des contrôleurs Meta Quest 2. De plus, l'interface utilisateur (UI 3D) s'intègre de manière totalement dans l'univers : pour l'étape finale du retour d'expérience (REX), le joueur n'interagit pas avec des menus classiques, mais utilise un **XR Ray Interactor** (pointeur laser dont la couleur change dynamiquement au survol des éléments interactifs) pour incrémenter ou décrémenter manuellement des boutons fléchés physiques de 0 à 9, afin de valider son code PIN sur l'ordinateur de bord.
2. **Utilisation des capteurs et des entrées pour enrichir l'expérience sensorielle :**
    
    Le projet tire pleinement parti du suivi des mouvements et de la reconnaissance d'entrées complexes pour simuler l'impact physique et sensoriel des handicaps :
    
    - **Suivi cinématique des mains et locomotion physique :** Dans la *Capsule de Mobilité Réduite*, le déplacement traditionnel par sticks a été supprimé. Le système détecte et interprète une combinaison de mouvements de rotations des mains du joueur sur les contrôleurs pour simuler la propulsion des roues d'un fauteuil roulant. Cette approche traduit la notion d'effort physique et de gestion d'inertie.
    - **Orientation lumineuse et masquage sensoriel :** Dans le laboratoire de chimie (*Capsule Dyslexie*), le suivi spatial des mains permet d'orienter une lumière directive selon les mouvements précis de l'utilisateur pour décoder les consignes. Face à l'éblouissement solaire qui perturbe l'écran toutes les 3 secondes, le joueur doit effectuer un geste de préhension physique réel pour fermer les rideaux de la pièce, liant l'activité motrice à la résolution d'une surcharge cognitive.

### c. Optimisation et Performance

Le maintien d'un taux de rafraîchissement élevé et constant est une condition sine qua non en réalité virtuelle pour assurer le confort visuel et valider la condition réglementaire de stabilité du jeu. Notre stratégie pour **OMOIYARI** s'est articulée autour de techniques d'ingénierie légères pour le processeur mobile du Meta Quest 2 et d'une phase de diagnostic :

1. **Techniques de réduction de la latence et optimisation matérielle :**
    
    Pour garantir une **latence critique inférieure à 20 ms** et des performances fluides, nous avons implémenté deux optimisations structurelles majeures sous Unity 6000 :
    
    - *Light Baking Statique (Gestion du GPU) :* Le calcul des lumières dynamiques et des ombres en temps réel étant extrêmement gourmand, nous avons procédé au *Baking* (pré-calcul) intégral des textures de lumière (*Lightmaps*) pour l'ensemble des scènes intérieures complexes (*Classroom*, *Cuisine*, et *Laboratoire de chimie*). Les ombres portées et les illuminations globales (Global illumination) sont ainsi directement figées dans les textures.
    - *Compression matérielle ASTC :* L'intégralité des textures de l'application (comme les textures de bois stylisées ou les matériaux du jardin japonais) a été convertie et compressée au format **ASTC (Adaptive Scalable Texture Compression)**.
2. **Utilisation d'outils de profilage pour évaluer et améliorer les performances :**
    
    L'évaluation et la validation de nos optimisations n'ont pas été empiriques, mais mesurées via des outils de diagnostic professionnels :
    
    - *Unity Profiler :* Cet outil nous a permis de monitorer en temps réel l'utilisation du CPU et du GPU pendant l'exécution des scènes.
    - *Oculus Link Performance HUD / Meta Quest Developer Hub (MQDH) :* En exécutant l'application sur le casque, nous avons utilisé le HUD de performance pour analyser le *Frame Rate* (cible à 72/90 FPS) ainsi que le *App Motion-to-Photon Latency*. Ce profilage ciblé nous a permis d'ajuster dynamiquement la distance d'affichage de la brume volumétrique (gérée par le plugin *CloudsToy*) dans la capsule du daltonisme, garantissant que l'intégration de cet effet spécial ne compromette jamais la fluidité globale, validant ainsi la stabilité du jeu bien au-delà des 30 premières secondes réglementaires.

### d. Rendu et Visualisation

Le rendu visuel dans OMOIYARI a fait l’objet d’un arbitrage entre la richesse esthétique inspirée de l'univers japonais traditionnel et les barrières matérielles strictes imposées par l’architecture mobile du Meta Quest 2. Notre travail s'est structuré autour de deux critères fondamentaux :

1. **Qualité du rendu visuel sous contraintes d'affichage VR :**
    
    L'affichage stéréoscopique en réalité virtuelle exige un taux de rafraîchissement constant et une netteté impeccable pour éviter l'effet de flou cinétique.
    
    - *Matériaux et Shaders optimisés :* Nous avons opté pour un style artistique *Low-Poly stylisé* s'appuyant sur des shaders de type *Vertex Lit* ou *Universal Render Pipeline (URP) / Lit* simplifiés. La texture de l'eau du jardin japonais a été entièrement programmée via le **S**hader Graph de Unity, permettant d'obtenir des reflets et des ondulations fluides par le biais de calculs mathématiques appliqués aux sommets (*vertices*), sans surcharger la mémoire de rendu.
2. **Évaluation de la cohérence entre les éléments virtuels et le monde réel :**
    
    Bien que l'expérience plonge le joueur dans un univers métaphorique et philosophique lié au concept du *Gaman*, la simulation repose sur le respect des lois physiques et des proportions du monde réel :
    
    - *Échelle 1:1 :* Une fois installé virtuellement dans le fauteuil roulant, perçoive le monde depuis la hauteur exacte d'une personne en situation de handicap moteur, rendant la sensation d'infériorité ou d'accessibilité vis-à-vis des objets parfaitement cohérente avec la réalité.
    - *Simulation de la gravité et interactions physiques :* La cohérence avec le monde réel se traduit également par le comportement des objets dynamiques. Grâce aux composants *Rigidbody* et aux *Mesh Colliders* simplifiés, les fioles de chimie de la capsule dyslexie ou les blocs en bois du puzzle Tangram réagissent fidèlement aux lois de la gravité lors de leur manipulation ou de leur chute. L'alignement de la lumière directive sur le suivi spatial des mains garantit une correspondance entre le geste physique réel de l'utilisateur et la réaction visuelle de son environnement virtuel, maximisant son sentiment de présence.

### **3. Critères d'Évaluation Artistique**

### a. Conception et Esthétique (Proposition Personnelle)

Notre proposition artistique s’éloigne volontairement du réalisme pour épouser un style **Low-Poly stylisé et** épuré. 

### 1. Cohérence esthétique des scènes VR et contribution à la narration visuelle

Pour lier nos 5 capsules sensorielles, nous avons créé une charte graphique et environnementale unique inspirée de l'univers traditionnel japonais. Les choix esthétiques ne sont pas seulement décoratifs, ils participent directement à la narration visuelle et à l'état psychologique du personnage:

- **La palette chromatique comme repère émotionnel :**Le *Rouge Impérial* (vigilance), les teintes de *Marrons chauds* (stabilité) et le *Rose Sakura* (sérénité).
- **Les éléments culturels comme métaphores de l'épreuve :** Le décor intègre des structures fortes telles que des portes *Torii* sacrées, des ponts en arc traditionnels, des lanternes en papier et une modélisation du *Mont Fuji* sculptée à la main.
- **L'espace de refuge diégétique :** Le concept du Gaman (我慢), l'art d'endurer les épreuves avec dignité et patience, les zones permettant d'apaiser le joueur et de faire redescendre sa jauge de stress (salle Autisme) prennent la forme de pavillons traditionnels japonais.

### 2. Originalité et créativité dans la conception des éléments visuels et interactifs

L'originalité de notre jeu repose sur sa capacité à transformer des contraintes d’handicaps en mécaniques de jeu interactives en VR:

- **Créativité visuelle (Les filtres de perception) :** Au lieu d'expliquer le daltonisme par du texte, nous avons créé une approche immersive originale en injectant des shaders de conversion chromatique (Deutéranopie/Tritanopie) directement sur la vue du joueur dans le casque. De même, la crise sensorielle de l'autisme est symbolisée par un trouble progressif de la caméra, suivi d'un fondu au noir total et d'une téléportation lorsque le stress atteint 100%. La vibration des contrôleurs, le bruit de l’orage participent à l’immersion.
- **Créativité interactive (L'effort matérialisé) :** L'implémentation de la locomotion pour la salle de mobilité réduite est le nouveau point central de notre projet : le joueur doit reproduire le mouvement circulaire réel des bras pour faire avancer son fauteuil roulant virtuel. De plus, pour le laboratoire de chimie (Dyslexie), l'interaction demande au joueur d'utiliser physiquement ses mains pour orienter une lumière directive afin de déchiffrer les consignes, ou de fermer les rideaux de la pièce pour stopper un éblouissement solaire cyclique toutes les 3 secondes. Les pièces du tangram respectent désormais un système de drag-drop grâce au far-near interactor.

### b. Immersion et Expérimentation

L'impact émotionnel de notre jeu repose sur sa capacité à isoler les sens du joueur pour simuler les barrières physiologiques des handicaps. Notre démarche artistique s'est structurée autour de l'implémentation de caractéristiques immersives et d'expérimentations sensorielles.

 **1. Qualité de l'immersion de l'utilisateur dans la scène VR (Au moins 2 éléments)** 

- Élément 1 : L'ancrage corporel et la présence physique diégétique
    
    Pour éviter le sentiment de flottement et maximiser l'incarnation, le joueur dispose d'un avatar virtuel complet où le châssis du fauteuil roulant ainsi que ses propres pieds restent visibles en permanence dans son champ de vision. Cet ancrage spatial renforce la sensation de handicap moteur.
    

 **2. Expérimentation avec des effets visuels et sonores uniques (Au moins 2 éléments)** 

- Élément 1 : Le design sonore spatialisé et l'audio 3D évolutif 
Le son est utilisé comme un vecteur d'angoisse. Dans la *Capsule Autisme*, nous expérimentons un environnement sonore évolutif : le bruit mécanique des roues, le vent directionnel et un grondement de tonnerre cyclique toutes les 5 secondes.
- Élément 2 : La synergie haptique et visuelle des crises cognitives 
Nous avons conçu une boucle d'effets visuels et physiques pour matérialiser la surcharge mentale. Lorsque le niveau de perturbation de l'utilisateur augmente, les manettes du Meta Quest 2 déclenchent des vibrations asynchrones et graduelles. Accompagné d'une téléportation au point de départ de la salle.
    
    # 3. Analyse détaillée capsule par capsule : L’implémentation de la VR
    
    Le cœur de l’expérience OMOIYARI repose sur son séquenceur de jeu divisé en 5 capsules distinctes. Chacune d’elles isole un trouble spécifique et s'articule autour d'une boucle de gameplay rigoureuse : 
    
    `Contrainte Sensorielle/Motrice ➔ Analyse Cognitive ➔ Action d'Adaptation Spatiale ➔ Validation par l'Empathie`
    
    ## Capsule 1 : La Mobilité Réduite (Handicap Moteur)
    
    ### La Mutation VR (Clavier-Souris ➔ Spatial)
    
    - **Ancienne version :** Le joueur déplaçait un avatar classique de manière abstraite à l'aide des flèches directionnelles du clavier.
    - **Version VR 2026 :** La locomotion est désormais indexée sur le suivi cinématique des mains du joueur.
    
    ### Déroulé et Interactions dans la Scène
    
    Le joueur est installé virtuellement dans un fauteuil roulant, qui lui sert de point d'ancrage visuel fixe.
    
    - **L'effort matérialisé :** Pour avancer, reculer ou pivoter, l'utilisateur doit effectuer des mouvements circulaires physiques et synchronisés avec ses bras à l’aide des contrôleurs.
    - **Le parcours d'obstacles :** Le joueur doit traverser une succession de dalles glissantes jouant sur l'inertie du fauteuil et réussir 3 épreuves rythmiques sous forme de Quick Time Events (QTE) complexes en moins de 3 secondes pour franchir les portes.
    - **L'action finale :** Une fois le parcours terminé, le joueur doit saisir physiquement une torche via le *XR Grab Interactor* (Trigger droit) et effectuer un mouvement réel pour embraser un brasier, ce qui déverrouille l'accès à la salle suivante.
    
    ![Capture d’écran 2026-05-25 à 06.04.51.png](Omoiyari%20-%20Experience%20immersive%20autour%20du%20handicap/Capture_decran_2026-05-25_a_06.04.51.png)
    
    [](https://www.notion.so)
    
    ## Capsule 2 : Le Daltonisme (Altération Sensorielle)
    
    ### La Mutation VR (Clavier-Souris ➔ Spatial)
    
    - **Ancienne version :** Une simple image ou un filtre global appliqué sur un écran plat 2D.
    - **Version VR 2026 :** Désormais le postprocessing est encore plus marqué par la proximité des yeux au filtre.
    
    ### Déroulé et Interactions dans la Scène
    
    Le joueur est au milieu d’une aire de jeux, un environnement initialement rassurant mais ici étouffé par une brume volumétrique générée par le plugin *CloudsToy*.
    
    - **La contrainte visuelle :** Des scripts mathématiques appliqués en temps réel sur la caméra de la *XR Origin* simulent la Deutéranopie (absence de perception du vert) ou la Tritanopie (absence de perception du bleu). Les repères de couleurs s'effacent.
    - **Le Puzzle Tangram spatialisé :** Pour progresser, l'utilisateur doit collecter des blocs en bois disséminés sous forme de triangles dans l'aire de jeux. Il doit ensuite résoudre un puzzle de Tangram en sélectionnant et en déplaçant physiquement les pièces géométriques en *Drag & Drop* via ses contrôleurs pour les encastrer parfaitement dans leurs socles respectifs.
    
    [](https://www.notion.so)
    
    ## Capsule 3 : Le Spectre Autistique (Surcharge Cognitive & Sensorielle)
    
    ### La Mutation VR (Clavier-Souris ➔ Spatial)
    
    - **Ancienne version :** Des indicateurs visuels à l'écran (jauges) et des bruitages stéréo classiques.
    - **Version VR 2026 :** Une agression sensorielle à 360° qui utilise l'audio spatialisé et les retours haptiques pour saturer l'attention du joueur.
    
    ### Déroulé et Interactions dans la Scène
    
    L'utilisateur est plongé dans un labyrinthe architectural obscur aux cloisons japonaises traditionnelles.
    
    - **La saturation sensorielle :** Toutes les 5 secondes, un coup de tonnerre retentit en audio spatialisé 3D, modifiant instantanément l'équilibre de l'espace sonore. En simultané, les manettes du Meta Quest 2 déclenchent des vibrations asynchrones et graduelles pour simuler l'augmentation de la jauge de stress interne. Des bruits de battements de coeurs s’intensifient.
    - **La perte de contrôle mécanique :** Si le joueur reste trop longtemps exposé au bruit, la jauge atteint un seuil critique : les commandes de déplacement s’inversent soudainement (le stick gauche va à droite, etc.), provoquant une désorientation immédiate.
    - **Les structures de secours :** Pour faire redescendre cette jauge de stress, le joueur doit localiser et se réfugier dans des pavillons traditionnels japonais (*maisons-refuges*), calmes et apaisants. Si le stress atteint 100 %, la vue devient progressivement trouble, subit un fondu au noir total et le joueur est téléporté au tout début du labyrinthe.
    
    ![Capture d’écran 2026-05-25 à 06.12.20.png](Omoiyari%20-%20Experience%20immersive%20autour%20du%20handicap/Capture_decran_2026-05-25_a_06.12.20.png)
    
    ## Capsule 4 : La Dyslexie Dynamique (Trouble Cognitif)
    
    ### La Mutation VR (Clavier-Souris ➔ Spatial)
    
    - **Ancienne version :** Un texte statique illisible affiché à l'écran qu'il fallait simplement lire.
    - **Version VR 2026 :** Maintenant on peut saisir le bêcher et verser les solutions dedans, le joueur peut renverser la solution si elle ne lui plaît pas, et la secouer s’il veut vérifier le précipité.
    
    ### Déroulé et Interactions dans la Scène
    
    Le joueur se retrouve dans un laboratoire de chimie face à un plan de travail et un grand tableau blanc contenant les instructions de l'expérience.
    
    - **La distorsion textuelle :** Les phrases écrites au tableau bougent, s'inversent et se déforment dynamiquement à l’écran, reproduisant la fatigue cognitive liée à une dyslexie sévère.
    - **L'éblouissement cyclique :** Toutes les 3 secondes, une lumière directive puissante (simulation de rayons solaires en *Ray Marching*) vient éblouir la pièce et rendre le tableau totalement invisible. Le joueur doit interrompre sa tâche, tendre le bras et effectuer et fermer le rideau à l’aide de son contrôleur et d’une touche.
    - **La manipulation physique :** Pour valider la salle, l'utilisateur doit suivre la recette scientifique en saisissant des béchers et des fioles de solutions chimiques avec ses mains virtuelles pour réaliser des mélanges.
    
    ![Capture d’écran 2026-05-25 à 06.17.06.png](Omoiyari%20-%20Experience%20immersive%20autour%20du%20handicap/Capture_decran_2026-05-25_a_06.17.06.png)
    
    ## Capsule 5 : Le Retour d'Expérience (REX & Quiz Final)
    
    ### La Mutation VR (Clavier-Souris ➔ Spatial)
    
    - **Ancienne version :** Un formulaire de texte standard ou un QCM cliquable sur un site web.
    - **Version VR 2026 : Le joueur doit désormais interagir avec une nouvelle interface pour transposer ses réponses au quiz directement avec les contrôleurs.**
    
    ### Déroulé et Interactions dans la Scène
    
    - **L'assimilation :** Le joueur fait face à des panneaux textuels et des fiches de synthèse récapitulant les réalités médicales et cliniques des handicaps qu'il vient d'incarner.
    - **Le Quiz d'évaluation :** Le joueur doit répondre à un questionnaire interactif afin de mettre un terme à la simulation.
    - **L'interface du Code PIN :** À l'aide de son pointeur laser, l'utilisateur doit viser et cliquer sur des flèches physiques en UI 3D pour incrémenter ou décrémenter manuellement des chiffres de 1 à 9 sur un ordinateur de bord virtuel. La saisie correcte de ce code PIN, calquée sur les réponses au quiz, déverrouille la clé de fin et clôture la session de sensibilisation.

[](https://www.notion.so)

---

# Conclusion

Pour conclure, ce travail marque la consécration de 2 années de travail sur notre projet de jeu sur Unity intitulé Omoiyari. Nous avons un jeu fonctionnel, dont le but est de sensibiliser aux handicaps, disponible en VR désormais. L’intégration des nouvelles fonctionnalités en VR rend l’immersion d’autant plus prenante, et laisse envisager des améliorations futures telles que la présence de mains, des handicaps tels que le TDAH, ou encore du AR.

Nous vous invitons à lire le GDD présent dans le GitHub sous forme de site [(https://zedroff.github.io/omoiyari-vr/)](https://zedroff.github.io/omoiyari-vr/), c’est une extension de ce document.

Un APK du jeu est présent au sein du GitHub. Vous pouvez l’intégrer directement dans votre casque meta quest 2

Lien du GitHub : [https://github.com/ZedRoff/omoiyari-vr](https://github.com/ZedRoff/omoiyari-vr)

Merci pour votre lecture.
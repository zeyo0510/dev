/*
 * Copyright (C) 2010-2022 Talend Inc. - www.talend.com
 *
 * This source code is available under agreement available at
 * https://www.talend.com/legal-terms/us-eula
 *
 * You should have received a copy of the agreement
 * along with this program; if not, write to Talend SA
 * 5 rue Salomon de Rothschild - 92150 Suresnes
 *
 */

/* eslint-disable no-param-reassign */ // TODO: remove and fix file

const KEYS = {
  UP: 38,
  DOWN: 40,
};

const scrollSmoothlyTo = (origin, to, durationInMS) => {
  if (durationInMS <= 0) {
    return;
  }

  const differenceX = to.offsetLeft - origin.scrollLeft;
  const differenceY = to.offsetTop - origin.scrollTop;
  const perTickX = (differenceX / durationInMS) * 10;
  const perTickY = (differenceY / durationInMS) * 10;

  setTimeout(() => {
    origin.scrollLeft += perTickX;
    origin.scrollTop += perTickY;
    if (origin.scrollTop === to) {
      return;
    }

    scrollSmoothlyTo(origin, to, durationInMS - 10);
  }, 10);
};

const scrollIntoContainerView = (element, classOfContainer) => {
  if (!element) {
    return;
  }

  // need to wrap that call into a setTimeout to make sure the dom has finished to render
  setTimeout(() => {
    const scrollableContainer = $(`.${classOfContainer}`);
    const scrollableHeight = scrollableContainer.height();
    const min = scrollableContainer.offset().top;
    const max = min + scrollableHeight;
    const elTop = $(element).offset().top;

    if (elTop <= min || elTop >= max) {
      scrollableContainer.animate({
        scrollTop: (elTop - min - (scrollableHeight / 2)) + scrollableContainer.scrollTop(),
      }, 800);
    }
  }, 0);
};

window.APP.utils = {
  scrollSmoothlyTo,
  scrollIntoContainerView,
  KEYS,
};
